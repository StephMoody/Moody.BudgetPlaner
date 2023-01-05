namespace Moody.PropertyChangedSourceGenerator
{
        public readonly struct PropertyChangedInfoToGenerate
        {
            public string Name { get; }
            public string Type { get; }

            public string FieldName { get; }

            public PropertyChangedInfoToGenerate(string name, string type, string fieldName)
            {
                Name = name;
                Type = type;
                FieldName = fieldName;
            }
        }
}