using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

enum MessageBoxButtons : long
{
    Ok = 0x00000000L,
    OkCancel = 0x00000001L,
    YesNo = 0x00000004L
}

public partial class Program
{
    static void Main(string[] args)
    {
        // MessageBox
        // {
        //     var ptr = Process.GetCurrentProcess().MainWindowHandle;
        //
        //     ShowMessageBox(ptr, "Hello system call", "System notification", (uint)MessageBoxButtons.Ok);
        //     ShowMessageBox(ptr, "Hello system call", "System notification", (uint)MessageBoxButtons.Ok);
        // }

        // {
        //     string windowTitle = "Task Manager";
        //
        //     var handle = GetWindowByTitle(windowTitle);
        //
        //     if (Marshal.GetLastWin32Error() == 0 && handle != IntPtr.Zero)
        //     {
        //         SetWindowTitle(handle, "Notepad++");
        //
        //         Console.WriteLine("Window title changed");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Window not found");
        //     }
        //
        //     IntPtr ptr = Marshal.AllocHGlobal(sizeof(int) * 20);
        //
        //     // Do something...
        //
        //     Marshal.FreeHGlobal(ptr);
        // }

        // {
        //     Process process = new Process();
        //     
        //     process.Exited += (sender, eventArgs) =>
        //     {
        //         ShowMessageBox(IntPtr.Zero, String.Empty, string.Empty, (uint)MessageBoxButtons.Ok);
        //     };
        //     
        //     process.StartInfo = new ProcessStartInfo()
        //     {
        //         FileName = "calc.exe"
        //     };
        //     
        //     process.Start();
        // }

        // {
        //     Process[] processes = Process.GetProcesses();
        //     
        //     foreach (var process in processes)
        //     {
        //         Console.WriteLine(process.ProcessName);
        //         
        //         try
        //         {
        //             Console.WriteLine(process.PriorityClass);
        //         }
        //         catch (Win32Exception ex)
        //         {
        //             Console.WriteLine("Access is denied!");
        //         }
        //     }
        //
        //     Console.ReadLine();
        // }

        {
            var info = new ProcessStartInfo();
            
            info.ArgumentList.Add("-a");
            info.ArgumentList.Add("/b");
            info.ArgumentList.Add("--value");
            info.ArgumentList.Add("--global");

            info.FileName =
                @"C:\Users\Alex\RiderProjects\FBES_3_22_3_ru_SP\Processes_2\bin\Debug\net8.0\Processes_2.exe";
            
            var process = Process.Start(info);

            Console.Read();
        }
    }

    [DllImport("user32.dll", EntryPoint = "MessageBox", CharSet = CharSet.Unicode)]
    public static extern int ShowMessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

    [DllImport("user32.dll", EntryPoint = "FindWindowW", CharSet = CharSet.Unicode, SetLastError = true)]
    public static extern IntPtr GetWindow(string? lpClassName, [MarshalAs(UnmanagedType.LPTStr)] string lpWindowName);

    public static IntPtr GetWindowByTitle(string title)
    {
        return GetWindow(null, title);
    }

    [DllImport("user32.dll", EntryPoint = "SetWindowTextW", CharSet = CharSet.Unicode)]
    public static extern bool SetWindowTitle(IntPtr hWnd, string lpString);
}