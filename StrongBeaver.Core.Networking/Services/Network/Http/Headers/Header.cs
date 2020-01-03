using System.Collections.Generic;

namespace StrongBeaver.Core.Services.Network.Http.Headers
{
    public class Header : IHeader
    {
        public Header(string name, string value)
        {
            Name = name;
            Value = new[] {value};
        }

        public Header(string name, params string[] value)
        {
            Name = name;
            Value = value;
        }

        public Header(string name, IEnumerable<string> value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }

        public IEnumerable<string> Value { get; }


    }
}