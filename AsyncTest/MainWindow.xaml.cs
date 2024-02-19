using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncTest;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        
    }

    private Task<string> MyButton_OnClick(object sender, RoutedEventArgs e)
    {
        var stream = new StreamReader("sample.txt");
        
        Task<string> result = stream.ReadToEndAsync();

        return result;
    }

    private async Task HandleRead(Task<string> task)
    {
        
    }
}