using System.Diagnostics;
using System.Threading;

// var process = new Process();
//
// process.PriorityClass = ProcessPriorityClass.RealTime;
// process.StartInfo.FileName = "calc.exe";
// process.ProcessorAffinity = 3;
//
// process.Start();


/// PT1

var threads = new List<Thread>();


for (int i = 0; i < 3; i++)
{
    var thread = new Thread(() =>
    {
        Thread.Sleep(1000);
        int threadId = Thread.CurrentThread.ManagedThreadId;
        Console.WriteLine($"Thread {threadId}: ");
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"{threadId}: {i}");
        }

        Console.WriteLine($"Thread {threadId} [end]");
    })
    {
        IsBackground = true
    };
    
    threads.Add(thread);
}

foreach (var thread in threads)
{
    thread.Start();
}

foreach (var thread in threads)
{
    thread.Join();
}

Console.WriteLine("main end");
Console.Read();
