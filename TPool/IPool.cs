namespace TPool;

interface IPool<T>
    where T : IPoolObject, new()
{
    T Get();
    void Remove(T @object);

    int Count { get; }
}