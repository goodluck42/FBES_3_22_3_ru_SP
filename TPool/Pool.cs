namespace TPool;

class Pool<T> : IPool<T>
    where T : IPoolObject, new()
{
    private readonly int _maxObjects;
    private T?[] _objects;

    public Pool(int maxObjects)
    {
        _maxObjects = maxObjects;
        _objects = new T?[maxObjects];
    }

    public T Get()
    {
        for (int i = 0; i < _maxObjects; i++)
        {
            if (_objects[i] == null)
            {
                _objects[i] = new T();

                return _objects[i]!;
            }

            if (_objects[i]!.Destroyed)
            {
                _objects[i]!.Destroyed = false;
                
                return _objects[i]!;
            }
        }

        throw new InvalidOperationException("Pool is full");
    }

    public int Count => _maxObjects;
    
    public void Remove(T @object)
    {
        int index = Array.IndexOf(_objects, @object);

        if (index != -1)
        {
            var obj = _objects[index]!;
            obj.Destroyed = true;
            obj.Reset();
        }
    }
}