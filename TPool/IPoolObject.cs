namespace TPool;

interface IPoolObject
{
    bool Destroyed { get; set; }
    void Reset();
}