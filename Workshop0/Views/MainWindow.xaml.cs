using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using Workshop0.ViewModels;

namespace Workshop0.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewCtrl
    {
        private readonly MainWindowVM _vm;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = _vm = new MainWindowVM(this);
        }

        private void ScriptCut_Executed(object sender, ExecutedRoutedEventArgs e) =>
            _vm.ScriptCut_Executed();
        private void ScriptCut_CanExecute(object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = _vm.ScriptCut_CanExecute();

        public void ShowError(string message, string title) => 
            MessageBox.Show(this, message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        
        public string? OpenFileDialog(string filter)
        {
            var ofn = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                InitialDirectory = Directory.GetCurrentDirectory(),
                Title = "Open Script Collection"
            };
            return ofn.ShowDialog(this) == true
                ? ofn.FileName
                : null;
        }

        public void Quit() => Close();
    }
}