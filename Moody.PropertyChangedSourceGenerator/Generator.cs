using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Moody.PropertyChangedSourceGenerator
{
    
    [Generator]
    public class Generator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(ctx => ctx.AddSource("GeneratePropertyChangedAttribute.g.cs",
                SourceText.From(SourceGenerationHelper.Attribute, Encoding.UTF8)));

            IncrementalValuesProvider<ClassDeclarationSyntax> fieldDeclarationSyntax =
                context.SyntaxProvider.CreateSyntaxProvider(predicate: FilterForClassesWithAttributeAndField(),
                    transform: GetSemanticTarget).Where(x => x is not null);
            
            IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilationAndEnums
                = context.CompilationProvider.Combine(fieldDeclarationSyntax.Collect());

            context.RegisterSourceOutput(compilationAndEnums,
                 (spc, source) => Execute(source.Item1, source.Item2, spc));
        }

        private void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax> classDeclarationSyntaxes, SourceProductionContext context)
        {
            if (classDeclarationSyntaxes.IsDefaultOrEmpty)
            {
                return;
            }
            
            List<ClassInfoForSourceGeneration> toGenerate = GetTypesToGenerate(compilation, classDeclarationSyntaxes, context.CancellationToken);
            
            if (toGenerate.Count <= 0)
                return;
            
            foreach (ClassInfoForSourceGeneration classInfo in toGenerate)
            {
                string result = SourceGenerationHelper.GeneratePartialClass(classInfo);
                context.AddSource($"{classInfo.Name}.g.cs", SourceText.From(result, Encoding.UTF8));
            }
        }

        private List<ClassInfoForSourceGeneration> GetTypesToGenerate(Compilation compilation, IEnumerable<ClassDeclarationSyntax> classDeclarationSyntaxes, CancellationToken cancellationToken)
        {
                List<ClassInfoForSourceGeneration> toGenerate = new();
                
                INamedTypeSymbol generatePropertyChangedAttributeSymbol = compilation.GetTypeByMetadataName("Moody.PropertyChangedSourceGenerator.GeneratePropertyChangedAttribute");
                if (generatePropertyChangedAttributeSymbol == null)
                {
                    return toGenerate;
                }
            
                foreach (ClassDeclarationSyntax classDeclarationSyntax in classDeclarationSyntaxes)
                {
                    cancellationToken.ThrowIfCancellationRequested();
            
                    SemanticModel semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
                    if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol typeSymbol)
                    {
                        continue;
                    }

                    List<PropertyChangedInfoToGenerate> propertyChangeInfosForGeneration = new();

                    IEnumerable<FieldDeclarationSyntax> fieldDeclarationsWithAttributes = classDeclarationSyntax.
                        Members.OfType<FieldDeclarationSyntax>().Where(x=>x.AttributeLists.Any());
                    
                    foreach (FieldDeclarationSyntax fieldDeclarationSyntaxWithAttribute in fieldDeclarationsWithAttributes)
                    {
                        foreach (AttributeListSyntax attributeListSyntax in fieldDeclarationSyntaxWithAttribute.AttributeLists)
                        {
                            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                            {
                                if (compilation.GetSemanticModel(attributeSyntax.SyntaxTree).GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                                {
                                    continue;
                                }

                                INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                                string fullName = attributeContainingTypeSymbol.ToDisplayString();

                                if (fullName != "Moody.PropertyChangedSourceGenerator.GeneratePropertyChangedAttribute")
                                    continue;
                                
                                
                                SeparatedSyntaxList<VariableDeclaratorSyntax> variableDeclaration = fieldDeclarationSyntaxWithAttribute.Declaration.Variables;
                                foreach(VariableDeclaratorSyntax variable in variableDeclaration)
                                {
                                    ISymbol declaredSymbol = compilation.GetSemanticModel(variable.SyntaxTree).GetDeclaredSymbol(variable);
                                    if (declaredSymbol is IFieldSymbol fieldInfo)
                                    {
                                        propertyChangeInfosForGeneration.Add(
                                            new PropertyChangedInfoToGenerate(
                                                variable.ToString().Substring(1, variable.ToString().Length - 1),
                                                fieldInfo.Type.Name, variable.ToString()));
                                    }
                                }
                            }
                        }
                    }

                    List<string> usingDeclarations = new();
                    if (classDeclarationSyntax.Parent is FileScopedNamespaceDeclarationSyntax fileScopedNamespaceDeclarationSyntax && 
                        fileScopedNamespaceDeclarationSyntax.Parent is CompilationUnitSyntax compilationUnitSyntax)
                    {
                        foreach (UsingDirectiveSyntax usingDirectiveSyntax in compilationUnitSyntax.Usings)
                        {
                            usingDeclarations.Add(usingDirectiveSyntax.ToString());
                        }
                    }
                    
                    toGenerate.Add(new ClassInfoForSourceGeneration(classDeclarationSyntax.Identifier.Text,
                        typeSymbol.ContainingSymbol.ToString(), propertyChangeInfosForGeneration,
                        typeSymbol.DeclaredAccessibility.ToString(), usingDeclarations));

                }
            
                return toGenerate;
        }

        private ClassDeclarationSyntax GetSemanticTarget(GeneratorSyntaxContext context, CancellationToken cancellationToken)
        {
            ClassDeclarationSyntax classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

            foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
            {
                foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
                {
                    if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                    {
                        continue;
                    }

                    INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                    string fullName = attributeContainingTypeSymbol.ToDisplayString();

                    if (fullName == "Moody.PropertyChangedSourceGenerator.ClassForGeneratePropertyChangedAttribute")
                    {
                        return classDeclarationSyntax;
                    }
                }
            }

            return null;
        }

        private Func<SyntaxNode, CancellationToken, bool> FilterForClassesWithAttributeAndField()
        { 
            return (node, _) =>
            {
                if (node is not ClassDeclarationSyntax classDeclarationSyntax || !classDeclarationSyntax.AttributeLists.Any()) 
                    return false;
                
                return classDeclarationSyntax.Members.OfType<FieldDeclarationSyntax>().Any(x => x.AttributeLists.Any());
            };
        }
    }
}