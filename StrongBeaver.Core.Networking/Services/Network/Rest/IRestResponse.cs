namespace StrongBeaver.Core.Services.Network.Rest
{
    public interface IRestResponse<out TData> : IResponse
    {
        TData Content { get; }
    }
}