namespace Graph
{
    public interface ILink<out T>
    {
        T Source { get; }
        T Target { get; }
    }
}
