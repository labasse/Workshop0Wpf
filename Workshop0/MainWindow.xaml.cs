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

namespace Workshop0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region File Menu
        private void MenuFileNew_Click(object sender, RoutedEventArgs e)
        {
            if (FindResource("user") is User user)
            {
                user.Login += "+";
            }
        }

        private void MenuFileQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}