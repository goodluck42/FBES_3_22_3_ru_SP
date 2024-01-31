// See https://aka.ms/new-console-template for more information

using var writer = new StreamWriter("data.txt");

foreach (string arg in args)
{
    writer.WriteLine(arg);
}
