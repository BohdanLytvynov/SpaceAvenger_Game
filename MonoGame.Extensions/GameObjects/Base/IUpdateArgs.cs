namespace MonoGame.Extensions.GameObjects.Base
{
    public interface IUpdateArgs
    {
    }

    public interface IUpdateArgs<T> : IUpdateArgs
    {
        public T Args { get; set; }
    }
}
