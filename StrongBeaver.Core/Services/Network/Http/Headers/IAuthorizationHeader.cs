namespace StrongBeaver.Core.Services.Network.Http.Headers
{
    public interface IAuthorizationHeader : IHeader
    {
        string Schema { get; }

        string Parameter { get; }
    }
}