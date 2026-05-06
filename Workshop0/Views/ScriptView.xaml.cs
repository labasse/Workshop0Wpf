using System.Windows.Controls;
using Workshop0.Models;
using Workshop0.Services;
using Workshop0.ViewModels;

namespace Workshop0.Views
{
    /// <summary>
    /// Logique d'interaction pour ScriptView.xaml
    /// </summary>
    public partial class ScriptView : UserControl
    {
        private ScriptVM _vm;
        public ScriptView()
        {
            InitializeComponent();
            DataContext = _vm = new ScriptVM(
                ServiceContainer.Instance.ScriptCurrencyService!
            );
        }
    }
}
