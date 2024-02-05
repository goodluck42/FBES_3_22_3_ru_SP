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
using System.Windows.Threading;

namespace SyncContext;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private SynchronizationContext _context;
    public MainWindow()
    {
        InitializeComponent();
        
        _context = SynchronizationContext.Current!;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        var thread = new Thread(() =>
        {
            Thread.Sleep(2000);
            
            _context.Send(o =>
            {
                ClickButton.Background = Brushes.Brown;
            }, null);
        });
        
        thread.Start();
    }
}