using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
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
            ScriptColl = ScriptCollection.InitTestData();
            User = new();
            DataContext = this;
            Filter = new FilterCommand(this, nameof(SelectedFilter));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICollectionView FilteredScripts
        {
            get;
            private set
            {
                if (field != value)
                {
                    field = value;
                    field.GroupDescriptions.Add(new PropertyGroupDescription(nameof(Script.Type)));
                    FilteredScripts.Filter = FilterScript;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredScripts)));
                }
            }
        }


        public ScriptCollection ScriptColl
        {
            get;

            [MemberNotNull(nameof(FilteredScripts))]
            private set
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScriptColl)));
                FilteredScripts = new CollectionViewSource() { Source = ScriptColl.Scripts }.View;
            }
        }

        public IEnumerable<ScriptType> ScriptTypes => Enum.GetValues<ScriptType>();

        public IEnumerable<ScriptType> ValidScriptTypes => ScriptTypes.Where(t=>t!=ScriptType.All);

        public ScriptType SelectedFilter { 
            get; 
            set 
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFilter)));
                    FilteredScripts.Filter = FilterScript;
                }
            }
        }

        private bool FilterScript(object script) =>
            SelectedFilter == ScriptType.All ||
            ((Script)script).Type == SelectedFilter;
        
        public User User { get; private set; }

        public Script? SelectedScript { 
            get;
            set
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedScript)));
            }
        } = null;

        #region File Menu
        public ICommand FileNew => new RelayCommand(
            _ => ScriptColl = new ScriptCollection(), 
            _ => ScriptColl.Scripts.Count > 0);

        public ICommand FileOpen => new RelayCommand(_ =>
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
        });

        public ICommand FileQuit => new RelayCommand(_ => Close());
        #endregion

        #region Edit Menu
        private void ScriptCut_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if(SelectedScript is not null)
                ScriptColl.Scripts.Remove(SelectedScript);
        }

        private void ScriptCut_CanExecute(object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = SelectedScript is not null; // TODO : Check if a script is selected and can be cut

        public ICommand Filter { get; private set; }

        private class FilterCommand : ICommand
        {
            private INotifyPropertyChanged _parent;
            private PropertyInfo _property;
            public FilterCommand(INotifyPropertyChanged parent, string filterProperty)
            {
                _parent = parent;
                _property = parent.GetType().GetProperty(filterProperty) 
                    ?? throw new ArgumentException($"Property {filterProperty} not found on {parent.GetType().Name}");
                _parent.PropertyChanged+=(s, e) =>
                {
                    if (e.PropertyName == filterProperty)
                        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                };
            }

            public event EventHandler? CanExecuteChanged; // A déclencher à chaque changement de filtre

            public bool CanExecute(object? parameter) => !parameter?.Equals(_property.GetValue(_parent)) ?? false;

            public void Execute(object? parameter) => _property.SetValue(_parent, parameter);
        }

        #endregion

        private void ProcessException(Exception ex, string action)
        {
            MessageBox.Show(this, $"Error {action}: {ex.Message}", action, MessageBoxButton.OK, MessageBoxImage.Error);
            // TODO : Log the stack trace and other details of the exception
        }
    }
}