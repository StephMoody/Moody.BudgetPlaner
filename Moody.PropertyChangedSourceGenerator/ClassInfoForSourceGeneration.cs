using System.Collections.Generic;

namespace Moody.PropertyChangedSourceGenerator
{
    public readonly struct ClassInfoForSourceGeneration
    {
        public ClassInfoForSourceGeneration(string name,
            string nameSpace,
            IEnumerable<PropertyChangedInfoToGenerate> propertyChangeInfosForGeneration,
            string accessibility,
            IEnumerable<string> usings)
        {
            Name = name;
            NameSpace = nameSpace;
            PropertyChangeInfosForGeneration = propertyChangeInfosForGeneration;
            Accessibility = accessibility;
            Usings = usings;
        }

        public string Name { get; }
        public string NameSpace { get; }

        public string Accessibility { get; }

        public IEnumerable<string> Usings { get; }

        public IEnumerable<PropertyChangedInfoToGenerate> PropertyChangeInfosForGeneration { get; }
    }
}