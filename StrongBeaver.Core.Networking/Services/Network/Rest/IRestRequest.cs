namespace StrongBeaver.Core.Services.Network.Rest
{
    public interface IRestRequest : IRequest
    {
        object Content { get; }
    }
}