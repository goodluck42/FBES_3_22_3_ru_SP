using Microsoft.VisualBasic.CompilerServices;

namespace MyPlugin;

public class LoggerPlugin
{
    private string _filename;
    
    public LoggerPlugin(string filename)
    {
        _filename = filename;
    }

    public void Log(LogType logType, string message)
    {
        File.AppendAllText(_filename, $"[{DateTime.Now}]/[{logType}]: {message}");
    }
}