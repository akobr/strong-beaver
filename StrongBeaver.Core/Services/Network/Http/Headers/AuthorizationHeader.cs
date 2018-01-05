using System.Collections.Generic;

namespace StrongBeaver.Core.Services.Network.Http.Headers
{
    public class AuthorizationHeader : IAuthorizationHeader
    {
        public AuthorizationHeader()
        {
            Name = HttpContants.HEADER_AUTHORIZATION;
        }

        public AuthorizationHeader(string schema, string parameter)
            : this()
        {
            Schema = schema;
            Parameter = parameter;
        }

        public string Name { get; }

        public string Schema { get; set; }

        public string Parameter { get; set; }

        public IEnumerable<string> Value => new[] {$"{Schema} {Parameter}"};
    }
}