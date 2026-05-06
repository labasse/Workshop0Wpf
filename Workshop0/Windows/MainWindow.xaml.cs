using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Workshop0.Services;
using Workshop0.ViewModels;

namespace Workshop0.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IUIService
    {
        public MainWindow()
        {
            ServiceContainer.Instance.UIService = this;
            ServiceContainer.Instance.ScriptCurrencyService = new ScriptCurrencyService();
            InitializeComponent();
        }

        public void ShowError(string message, string title) =>
            MessageBox.Show(this, message, title, MessageBoxButton.OK, MessageBoxImage.Error);

        public string? OpenFileDialog(string filter)
        {
            var ofn = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = $"{filter}|All files (*.*)|*.*",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "Open Script Collection"
            };
            return ofn.ShowDialog(this) == true
                ? ofn.FileName
                : null;
        }
        public void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}