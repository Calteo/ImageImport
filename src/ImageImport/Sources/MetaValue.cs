using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageImport.Sources
{
    internal class MetaValue
    {
        public MetaValue(string name, object value, object baseValue, string description)
        {
            Name = name;
            Value = value;
            BaseValue = baseValue;
            Description = description;
        }

        public string Name { get; }
        public object Value { get; }
        public object BaseValue { get; }
        public string Description { get; }
    }
}
