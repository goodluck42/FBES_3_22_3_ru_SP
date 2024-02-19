// Task.Factory.StartNew(() =>
// {
//     while (true)
//     {
//         
//     }
// });


// foreach (var arg in args)
// {
//     Console.WriteLine($"arg: {arg}");
// }
//
// var task1 = Task.Run(async () =>
// {
//     Console.WriteLine($"task1 threadId: {Thread.CurrentThread.ManagedThreadId}");
//     var rnd = Random.Shared.Next(10, 42);
//     await Task.Delay(Random.Shared.Next(500, 1500));
//     Console.WriteLine($"task1: {rnd}");
//     
//     return rnd;
// });
//
// var task2 = Task.Run(async () =>
// {
//     Console.WriteLine($"task2 threadId: {Thread.CurrentThread.ManagedThreadId}");
//     
//     var rnd = Random.Shared.Next(10, 42);
//     await Task.Delay(Random.Shared.Next(500, 1500));
//     Console.WriteLine($"task2: {rnd}");
//     
//     return rnd;
// });
//
// var task3 = Task.Run(async () =>
// {
//     Console.WriteLine($"task3 threadId: {Thread.CurrentThread.ManagedThreadId}");
//     var rnd = Random.Shared.Next(10, 42);
//     await Task.Delay(Random.Shared.Next(500, 1500));
//     Console.WriteLine($"task3: {rnd}");
//     
//     return rnd;
// });
//
// {
//
//     var result1 = await task1;
//     Console.WriteLine("task1 cmp");
//     var result2 = await task2;
//     Console.WriteLine("task2 cmp");
//     var result3 = await task3;
//     Console.WriteLine("task3 cmp");
//
//     Console.WriteLine(result1 + result2 + result3);
// }
//
//
// for (int i = 0; i < 10; i++)
// {
//     Thread.Sleep(100);
//     Console.WriteLine(i);
// }
//
// Console.Read();


using System.Threading.Channels;

var service = new Service();


service.DoOperation();

Console.WriteLine("Press enter to continue");

if (Console.ReadKey().Key == ConsoleKey.Enter)
{
    service.Stop();
    
    try
    {
        var currentTask = service.ActiveTask;

        await currentTask!;
    }
    catch (OperationCanceledException ex)
    {
        Console.WriteLine($"OperationCanceledException: {ex.Message}");
    }
    finally
    {
        Console.WriteLine("Task canceled");
    }
}

class Service
{
    public Task? ActiveTask { get; private set; }
    private CancellationTokenSource _cts;
    private CancellationToken _ct;

    public Service()
    {
        _cts = new CancellationTokenSource();
        _ct = _cts.Token;
    }

    public void DoOperation()
    {
        ActiveTask = Task.Factory.StartNew(async () =>
        {
            int i = 0;

            while (true)
            {
                if (_ct.IsCancellationRequested)
                {
                    _ct.ThrowIfCancellationRequested();
                }
                
                await Task.Delay(500);

                Console.WriteLine(i++);
            }
        }, _ct, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    public void Stop()
    {
        _cts.Cancel();
    }
}
//
//
// var apis = new List<Task<int>>();
//
// async Task<int> ApiCall(int index)
// {
//     var returnValue = Random.Shared.Next();
//
//     Console.WriteLine($"[{index}]: {returnValue}");
//     
//     await Task.Delay(Random.Shared.Next(1000, 3000));
//
//     Console.WriteLine($"[{index}] done");
//
//     return returnValue;
// }
//
// apis.Add(Task.Run(() => ApiCall(0)));
// apis.Add(Task.Run(() => ApiCall(1)));
// apis.Add(Task.Run(() => ApiCall(2)));
// apis.Add(Task.Run(() => ApiCall(3)));
//
// // {
// //     var result = await (await Task.WhenAny(apis));
// //
// //     Console.WriteLine($"result = {result}");
// // }
//
// // {
// //     var results = await Task.WhenAll(apis);
// //
// //     for (int i = 0; i < results.Length; i++)
// //     {
// //         var result = results[i];
// //         
// //         Console.WriteLine(result);
// //     }
// // }
//
// {
//     var taskArray = apis.ToArray();
//     var task = Task.Run(async () =>
//     {
//         await Task.Delay(1000);
//
//         Console.WriteLine("Task completed!");
//     });
//     
//     await Task.WhenAll(taskArray);
//     await task;
// }
//
// Console.Read();