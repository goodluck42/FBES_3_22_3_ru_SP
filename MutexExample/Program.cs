if (!Mutex.TryOpenExisting("MyMutex", out var mutex))
{
    Console.WriteLine("Mutex not found");

    return;
}

var thread1 = new Thread(() =>
{
    Console.WriteLine($"Thread: thread2 before WaitOne");
    mutex.WaitOne();
    Console.WriteLine($"Thread: thread2 after WaitOne");

    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine(i);
        Thread.Sleep(1000);
    }
    
    mutex.ReleaseMutex();

    Console.WriteLine($"Thread: thread2 ReleaseMutex");
})
{
    IsBackground = true
};

thread1.Start();

thread1.Join();

Console.ReadKey();