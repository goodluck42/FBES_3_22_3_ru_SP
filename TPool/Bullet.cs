namespace TPool;

class Bullet : IPoolObject
{
    private int x = 0;
    private int y = 0;

    public Bullet()
    {
        Console.WriteLine("Bullet created");
    }

    public bool Destroyed { get; set; }

    public void Reset()
    {
        x = 0;
        y = 0;
    }

    public void Hit()
    {
        Console.WriteLine("got hit ");
    }
}