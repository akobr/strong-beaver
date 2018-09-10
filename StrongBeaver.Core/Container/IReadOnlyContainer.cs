namespace StrongBeaver.Core.Container
{
    public interface IReadOnlyContainer
    {
        TType GetInstance<TType>();

        TType GetInstance<TType>(string key);

        TService GetInstanceWithoutCaching<TService>();
    }
}