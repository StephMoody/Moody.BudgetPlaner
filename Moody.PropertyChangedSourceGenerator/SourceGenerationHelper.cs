using System.Text;

namespace Moody.PropertyChangedSourceGenerator
{
    public class SourceGenerationHelper
    {
            public const string Attribute = @"
namespace Moody.PropertyChangedSourceGenerator
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class GeneratePropertyChangedAttribute : System.Attribute
    {
    }

    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class ClassForGeneratePropertyChangedAttribute : System.Attribute
    {
    }
}";

            public static string GeneratePartialClass(ClassInfoForSourceGeneration classInfoForSourceGeneration)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string usings in classInfoForSourceGeneration.Usings)
                {
                    sb.AppendLine(usings);
                }
                
                sb.AppendLine($"");
                
                sb.AppendLine($"namespace {classInfoForSourceGeneration.NameSpace}");
                sb.AppendLine("{");
                sb.AppendLine($"    {classInfoForSourceGeneration.Accessibility.ToLower()} partial class {classInfoForSourceGeneration.Name}");
                sb.AppendLine("     {");
                sb.AppendLine($"");

                foreach (PropertyChangedInfoToGenerate propertyChangedInfoToGenerate in classInfoForSourceGeneration.PropertyChangeInfosForGeneration)
                {
                    sb.AppendLine($"");
                    sb.AppendLine($"public {propertyChangedInfoToGenerate.Type} {propertyChangedInfoToGenerate.Name}");
                    sb.AppendLine("{");
                    sb.AppendLine($"        get => {propertyChangedInfoToGenerate.FieldName};");
                    sb.AppendLine("        set");
                    sb.AppendLine("        {");
                    sb.AppendLine($"            if (value.Equals({propertyChangedInfoToGenerate.FieldName})) return;");
                    sb.AppendLine($"            {propertyChangedInfoToGenerate.FieldName} = value;");
                    sb.AppendLine("            OnPropertyChanged();");
                    sb.AppendLine("        }");
                    sb.AppendLine("}");
                }
                
                sb.AppendLine("     }");
                sb.AppendLine("}");

                return sb.ToString();
            }
    }
}