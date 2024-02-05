using System.Threading.Channels;
using TPool;

///// Pool
// var pool = new Pool<Bullet>(5);
// var usedBullets = new Bullet[pool.Count];
//
// for (int i = 0; i < pool.Count; i++)
// {
//     Bullet bullet = pool.Get();
//     
//     bullet.Hit();
//
//     usedBullets[i] = bullet;
// }
//
//
// foreach (var usedBullet in usedBullets)
// {
//     pool.Remove(usedBullet);
// }
//
//
// for (int i = 0; i < pool.Count; i++)
// {
//     Bullet bullet = pool.Get();
//     
//     bullet.Hit();
//     
//     usedBullets[i] = bullet;
// }

////// ThreadPool
// ThreadPool.GetMinThreads(out int minSync, out int minAsync);
// ThreadPool.GetMaxThreads(out int maxSync, out int maxAsync);
//
// Console.WriteLine($"{nameof(minSync)}: {minSync}");
// Console.WriteLine($"{nameof(maxSync)}: {maxSync}");
//
// Console.WriteLine($"{nameof(minAsync)}: {minAsync}");
// Console.WriteLine($"{nameof(maxAsync)}: {maxAsync}");
//
//
//
// for (int j = 0; j < 3; j++)
// {
//     ThreadPool.QueueUserWorkItem(o =>
//     {
//         Console.WriteLine($"ManagedThreadId: {Thread.CurrentThread.ManagedThreadId}");
//         for (int i = 0; i < 5; i++)
//         {
//             Console.WriteLine(i);
//         }
//     });
// }


///// Parallel
var arr = new List<int>();

for (int i = 0; i < 10; i++)
{
    arr.Add(Random.Shared.Next(10, 99));

    Console.WriteLine(arr[i]);
}

Console.WriteLine("--------------------");

// Parallel.For(0, arr.Count, (index, state) =>
// {
//     Console.WriteLine($"Id: {Thread.CurrentThread.ManagedThreadId}");
//     Console.WriteLine(arr[index]);
// });


// Parallel.ForEach<int>(arr, (value, state) =>
// {
//     Console.WriteLine($"Id: {Thread.CurrentThread.ManagedThreadId}");
//     Console.WriteLine(value);
// });


// Action action1 = () =>
// {
//     Console.WriteLine("Action 1");
// }, action2 = () =>
// {
//     Console.WriteLine("Action 2");
// }, action3 = () =>
// {
//     Console.WriteLine("Action 3");
// };
//
// Parallel.Invoke(action1, action2, action3);

var thread = new Thread(() =>
{
    for (int i = 0; i < 10; i++)
    {
        if (i == 5)
        {
            Console.WriteLine(2 / int.Parse(Console.ReadLine()!));
        }
    }
});

thread.Start();

Console.Read();

Console.WriteLine("done");
