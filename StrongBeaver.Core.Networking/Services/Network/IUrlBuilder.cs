using System;
using System.Collections.Generic;

namespace StrongBeaver.Core.Services.Network
{
    public interface IUrlBuilder : IFluentSyntax
    {
        IUrlBuilder Scheme(string schema);

        IUrlBuilder Host(string host);

        IUrlBuilder Port(int port);

        IUrlBuilder Path(string path);

        IUrlBuilder CombinePath(string segment);

        IUrlBuilder Query(string query);

        IUrlBuilder Fragment(string fragment);

        IUrlBuilder QueryParameter(string name, object value);

        IUrlBuilder QueryParameters(IReadOnlyDictionary<string, object> parameters);

        IUrlBuilder RemoveQueryParameter(string name);

        IUrlBuilder ClearQueryParameters();

        Uri BuildUrl();
    }
}