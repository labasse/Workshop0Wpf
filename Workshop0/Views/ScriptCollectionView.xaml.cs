using System;
using System.Collections.Generic;
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
using Workshop0.Services;
using Workshop0.ViewModels;

namespace Workshop0.Views
{
    /// <summary>
    /// Logique d'interaction pour ScriptCollectionView.xaml
    /// </summary>
    public partial class ScriptCollectionView : UserControl
    {
        private readonly ScriptCollectionVM _vm;

        public ScriptCollectionView()
        {
            InitializeComponent();
            DataContext = _vm = new ScriptCollectionVM(
                ServiceContainer.Instance.UIService!,
                ServiceContainer.Instance.ScriptCurrencyService!
            );
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e) =>
            _vm.FileNew.Execute(e.Parameter);
        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = _vm.FileNew.CanExecute(e.Parameter);

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e) =>
            _vm.FileOpen.Execute(e.Parameter);
        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = _vm.FileOpen.CanExecute(e.Parameter);

        private void ImportDb_Executed(object sender, ExecutedRoutedEventArgs e) =>
            _vm.ImportDb.Execute(e.Parameter);
        private void ImportDb_CanExecute(object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = _vm.ImportDb.CanExecute(e.Parameter);

        private void ExportDb_Executed(object sender, ExecutedRoutedEventArgs e) =>
            _vm.ExportDb.Execute(e.Parameter);
        private void ExportDb_CanExecute(object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = _vm.ExportDb.CanExecute(e.Parameter);

        private void ScriptCut_Executed(object sender, ExecutedRoutedEventArgs e) =>
            _vm.ScriptCut_Executed();
        private void ScriptCut_CanExecute(object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = _vm.ScriptCut_CanExecute();

        private void Filter_Executed(object sender, ExecutedRoutedEventArgs e) =>
            _vm.Filter.Execute(e.Parameter);
        
        private void Filter_CanExecute(object sender, CanExecuteRoutedEventArgs e) =>
             e.CanExecute = _vm.Filter.CanExecute(e.Parameter);        
    }
}
