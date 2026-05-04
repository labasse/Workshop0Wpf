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
using System.Xml.Serialization;

namespace Workshop0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ScriptCollection? _scripts = null;

        public MainWindow()
        {
            InitializeComponent();
            _scripts = new ScriptCollection() { Path = "" };
            _scripts.InitTestData();
            DataContext = _scripts;
        }

        #region File Menu
        private void MenuFileNew_Click(object sender, RoutedEventArgs e)
        {
            if (FindResource("user") is User user)
            {
                user.Login += "+";
            }
        }

        private void MenuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuFileQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}