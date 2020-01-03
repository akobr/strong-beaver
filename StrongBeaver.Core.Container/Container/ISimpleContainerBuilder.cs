namespace StrongBeaver.Core.Container
{
    public interface ISimpleContainerBuilder
    {
        bool Writable { get; set; }

        ISimpleContainer CreateContainer();
    }
}