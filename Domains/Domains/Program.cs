using System.Reflection;
using System.Runtime.Loader;

var asmLoadContext = new AssemblyLoadContext("MyContext", true);

//asmLoadContext.LoadFromAssemblyName(AssemblyName.GetAssemblyName("System.Core"));
using var fileStream = new FileStream("MyPlugin.dll", FileMode.Open, FileAccess.Read);

var pluginAssembly = asmLoadContext.LoadFromStream(fileStream);
var types = pluginAssembly.GetTypes();
var loggerClassType = types.FirstOrDefault(t => t.FullName == "MyPlugin.LoggerPlugin")!;
var loggerMethods = loggerClassType.GetMethods();
var logMethod = loggerMethods.FirstOrDefault(m => m.Name == "Log")!;
var loggerObject = Activator.CreateInstance(loggerClassType, "logs.txt");
var logTypeEnumType = types.FirstOrDefault(t => t.FullName == "MyPlugin.LogType")!;

// args for log method
var enumObject = Enum.Parse(logTypeEnumType, "Info");
var message = "Умид хватит сидеть в телефоне!";

logMethod.Invoke(loggerObject, [enumObject, message]);

asmLoadContext.Unload();

Console.Read();