using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace StrongBeaver.Core.Services.Network
{
    public class UrlBuilder : IUrlBuilder
    {
        private const char QUERY_START_SYMBOL = '?';
        private const char QUERY_DELIMITER_SYMBOL = '&';
        private const char QUERY_PARAMETER_ASSIGN_SYMBOL = '=';

        private readonly UriBuilder builder;
        private readonly Dictionary<string, string> queryParameters;

        public UrlBuilder(UriBuilder builder)
        {
            this.builder = builder;
            queryParameters = new Dictionary<string, string>();
            DecodeQueryParameters();
        }

        public UrlBuilder()
         : this(new UriBuilder())
        {
            // no operation
        }

        public UrlBuilder(Uri url)
            : this(new UriBuilder(url))
        {
            // no operation
        }

        public UrlBuilder(string url)
            : this(new UriBuilder(url))
        {
            // no operation
        }

        public UrlBuilder(string urlFormat, params object[] urlArguments)
            : this(string.Format(urlFormat, CultureInfo.InvariantCulture, EncodeValues(urlArguments)))
        {
            // no operation
        }

        public IUrlBuilder Scheme(string schema)
        {
            builder.Scheme = schema;
            return this;
        }

        public IUrlBuilder Host(string host)
        {
            builder.Host = host;
            return this;
        }

        public IUrlBuilder Port(int port)
        {
            builder.Port = port;
            return this;
        }

        public IUrlBuilder Path(string path)
        {
            builder.Path = path;
            return this;
        }

        public IUrlBuilder CombinePath(string segment)
        {
            builder.Path = System.IO.Path.Combine(builder.Path, segment);
            return this;
        }

        public IUrlBuilder Query(string query)
        {
            builder.Query = query;
            DecodeQueryParameters();
            return this;
        }

        public IUrlBuilder Fragment(string fragment)
        {
            builder.Fragment = fragment;
            return this;
        }

        public IUrlBuilder QueryParameter(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return this;
            }

            string encodedName = Uri.EscapeUriString(name);
            queryParameters[encodedName] = EncodeValue(value);
            return this;
        }

        public IUrlBuilder QueryParameters(IReadOnlyDictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count < 1)
            {
                return this;
            }

            foreach (var parameter in parameters)
            {
                QueryParameter(parameter.Key, parameter.Value);
            }

            return this;
        }

        public IUrlBuilder RemoveQueryParameter(string name)
        {
            queryParameters.Remove(Uri.EscapeUriString(name));
            return this;
        }

        public IUrlBuilder ClearQueryParameters()
        {
            queryParameters.Clear();
            return this;
        }

        public Uri BuildUrl()
        {
            EncodeQueryParameters();
            return builder.Uri;
        }

        private void DecodeQueryParameters()
        {
            string query = builder.Query;

            if (query.Length < 2)
            {
                return;
            }

            string[] parameters = query.Split(new char[] {QUERY_START_SYMBOL, QUERY_DELIMITER_SYMBOL}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in parameters)
            {
                int indexOfAssignSymbol = parameter.IndexOf(QUERY_PARAMETER_ASSIGN_SYMBOL);

                if (indexOfAssignSymbol < 1)
                {
                    continue;
                }

                string name = parameter.Substring(0, indexOfAssignSymbol);
                int valueStartIndex = indexOfAssignSymbol + 1;
                string value = parameter.Substring(valueStartIndex, parameter.Length - valueStartIndex);

                queryParameters[name] = value;
            }
        }

        private void EncodeQueryParameters()
        {
            if (queryParameters.Count < 1)
            {
                builder.Query = string.Empty;
                return;
            }

            StringBuilder queryBuilder = new StringBuilder();
            queryBuilder.Append(QUERY_START_SYMBOL);

            foreach (var parameter in queryParameters)
            {
                queryBuilder.Append(parameter.Key);
                queryBuilder.Append(QUERY_PARAMETER_ASSIGN_SYMBOL);
                queryBuilder.Append(parameter.Value);
                queryBuilder.Append(QUERY_DELIMITER_SYMBOL);
            }

            queryBuilder.Remove(queryBuilder.Length - 1, 1);
            builder.Query = queryBuilder.ToString();
        }

        private static string[] EncodeValues(IReadOnlyList<object> values)
        {
            if (values == null || values.Count < 1)
            {
                return new string[0];
            }

            string[] result = new string[values.Count];

            for (int i = 0; i < values.Count; i++)
            {
                result[i] = EncodeValue(values[i]);
            }

            return result;
        }

        private static string EncodeValue(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string stringifiedValue = value is IFormattable formattableArgument
                ? formattableArgument.ToString(null, CultureInfo.InvariantCulture)
                : value.ToString();

            return Uri.EscapeDataString(stringifiedValue);
        }
    }
}
