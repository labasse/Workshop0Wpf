using System.ComponentModel;
using System.IO;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            ScriptColl = new();
            User = new();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ScriptCollection ScriptColl
        {
            get => field;
            private set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScriptColl)));
                }
            }
        }

        public IEnumerable<ScriptType> ScriptTypes => Enum.GetValues<ScriptType>();

        public User User { get; private set; }

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
            var ofn = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "Open Script Collection"
            };
            if (ofn.ShowDialog(this) ?? false)
            {
                try
                {
                    using var stream = new FileStream(ofn.FileName, FileMode.Open, FileAccess.Read);

                    try
                    {
                        ScriptColl = ScriptCollection.Load(stream);
                    }
                    catch (Exception xEx)
                    {
                        ProcessException(xEx, "deserializing XML");
                    }
                }
                catch (IOException xIO)
                {
                    ProcessException(xIO, "opening file");
                }
            }
        }
        private void MenuFileQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        private void ProcessException(Exception ex, string action)
        {
            MessageBox.Show(this, $"Error {action}: {ex.Message}", action, MessageBoxButton.OK, MessageBoxImage.Error);
            // TODO : Log the stack trace and other details of the exception
        }
    }
}