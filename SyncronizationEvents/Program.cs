using System.Threading.Channels;

// var @event = new ManualResetEvent(false);
//
// Console.WriteLine(@event);
//
// var thread1 = new Thread(() =>
// {
//     @event.WaitOne();
//     Console.WriteLine($"Id: thread1");
//     for (int i = 100; i < 120; i++)
//     {
//         @event.WaitOne();
//         Console.WriteLine(i);
//         Thread.Sleep(1000);
//     }
//
//     Console.WriteLine($"Id: thread1 [end]");
// });
//
// var thread2 = new Thread(() =>
// {
//     @event.WaitOne();
//     Console.WriteLine($"Id: thread2");
//     for (int i = 120; i < 140; i++)
//     {
//         @event.WaitOne();
//         Console.WriteLine(i);
//         Thread.Sleep(1000);
//     }
//     
//     Console.WriteLine($"Id: thread2 [end]");
// });
//
// var thread3 = new Thread(() =>
// {
//     bool flag = true;
//     while (flag)
//     {
//         string b = Console.ReadLine()!;
//
//         switch (b)
//         {
//             case "start":
//                 @event.Set();
//                 break;
//             case "stop":
//                 @event.Reset();
//                 break;
//             default:
//                 flag = false;
//                 break;
//         }
//     }
// });

// thread1.Start();
// thread2.Start();
// thread3.Start();
//
// thread1.Join();
// thread2.Join();
// thread3.Join();


var @event = new AutoResetEvent(false);

Console.WriteLine(@event);

var thread1 = new Thread(() =>
{
    @event.WaitOne();
    Console.WriteLine($"Id: thread1");
    for (int i = 100; i < 120; i++)
    {
        Console.WriteLine(i);
        Thread.Sleep(300);
    }
    Console.WriteLine($"Id: thread1 [end]");
    @event.Set();
});

var thread2 = new Thread(() =>
{
    @event.WaitOne();
    Console.WriteLine($"Id: thread2");
    for (int i = 120; i < 140; i++)
    {
        Console.WriteLine(i);
        Thread.Sleep(300);
    }

    Console.WriteLine($"Id: thread2 [end]");
    @event.Set();
});

var thread3 = new Thread(() =>
{
    @event.WaitOne();
    Console.WriteLine($"Id: thread3");
    for (int i = 140; i < 160; i++)
    {
        Console.WriteLine(i);
        Thread.Sleep(300);
    }

    Console.WriteLine($"Id: thread3 [end]");
    @event.Set();
});

@event.Set();

thread1.Start();
thread2.Start();
thread3.Start();

thread1.Join();
thread2.Join();
thread3.Join();

Console.Read();

var s = new BinaryReader(new FileStream());


// RLE
byte[] data = {97, 97, 97, 65, 65, 65};

byte[] archived = {3, 97, 3, 65};