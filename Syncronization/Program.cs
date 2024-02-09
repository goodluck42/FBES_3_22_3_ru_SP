using System.Collections.Concurrent;

// var locker = new object();
//
// // bool locker = false;
// var thread1 = new Thread(() =>
// {
//     Console.WriteLine($"Thread: thread1 before Monitor.Enter");
//     lock (locker)
//     {
//         Console.WriteLine($"Thread: thread1 after Monitor.Enter");
//
//         // while (locker) {}
//         // locker = true;
//         for (int i = 0; i < 5; i++)
//         {
//             Console.WriteLine(i);
//         }
//     }
//     // locker = false
//
//     Console.WriteLine($"Thread: thread1 Monitor.Exit");
// })
// {
//     IsBackground = true
// };
//
// var thread2 = new Thread(() =>
// {
//     Console.WriteLine($"Thread: thread2 before Monitor.Enter");
//     lock (locker)
//     {
//         Console.WriteLine($"Thread: thread2 after Monitor.Enter");
//         // while (locker) {}
//         // locker = true
//         for (int i = 5; i <= 10; i++)
//         {
//             Console.WriteLine(i);
//         }
//     }
//     // locker = false
//
//     Console.WriteLine($"Thread: thread2 Monitor.Exit");
// })
// {
//     IsBackground = true
// };
//
// thread1.Start();
// thread2.Start();
//
// thread1.Join();
// thread2.Join();
//
// Console.ReadKey();

//// Thread safety


// var concurrentList = new ConcurrentBag<int>();
// var list = new List<int>();
//
// var thread1 = new Thread(() =>
// {
//     for (int i = 0; i < 10000; i++)
//     {
//         list.Add(Random.Shared.Next());
//     }
// });
//
// var thread2 = new Thread(() =>
// {
//     for (int i = 0; i < 10000; i++)
//     {
//         list.Add(Random.Shared.Next());
//     }
// });
//
// thread1.Start();
// thread2.Start();
//
// thread1.Join();
// thread2.Join();


// Mutex

// var mutex = new Mutex(true, "MyMutex");
//
// var thread1 = new Thread(() =>
// {
//     Console.WriteLine($"Thread: thread1 before WaitOne");
//     mutex.WaitOne();
//     Console.WriteLine($"Thread: thread1 after WaitOne");
//
//     for (int i = 0; i < 5; i++)
//     {
//         Console.WriteLine(i);
//         Thread.Sleep(1000);
//     }
//     
//     mutex.ReleaseMutex();
//
//     Console.WriteLine($"Thread: thread1 ReleaseMutex");
// })
// {
//     IsBackground = true
// };
//
// Console.WriteLine("Press Enter to start!");
//
// var keyInfo = Console.ReadKey();
//
// if (keyInfo.Key == ConsoleKey.Enter)
// {
//     mutex.ReleaseMutex();
//     
//     thread1.Start();
//
//     thread1.Join();
//
//     Console.ReadKey();
// }
// Semaphore
// var semaphore = new Semaphore(3, 3);
//
// var tasks = new List<Thread>();
//
// for (int i = 0; i < 12; i++)
// {
//     tasks.Add(new Thread(UploadFile));
// }
//
// foreach (var thread in tasks)
// {
//     thread.Start();
// }
//
// foreach (var thread in tasks)
// {
//     thread.Join();
// }
//
// void UploadFile()
// {
//     semaphore.WaitOne();
//     Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: File upload started!");
//     Thread.Sleep(3000); // File upload
//     semaphore.Release();
//     Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}: File upload done!");
// }



var value = 0;
var thread1 = new Thread(() =>
{
    for (int i = 0; i < 100000; i++)
    {
        Interlocked.Increment(ref value);
    }
})
{
    IsBackground = true
};

var thread2 = new Thread(() =>
{
    for (int i = 0; i < 100000; i++)
    {
        Interlocked.Increment(ref value);
    }
})
{
    IsBackground = true
};


thread1.Start();
thread2.Start();

thread1.Join();
thread2.Join();

Console.WriteLine(value);

Console.ReadKey();