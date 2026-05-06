using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Workshop0.Models;
using Workshop0.Services;
using Workshop0.ViewModels.Commands;

namespace Workshop0.ViewModels
{
    public class ScriptCollectionVM : INotifyPropertyChanged
    {
        private IUIService _uiService;
        private IScriptCurrencyService _scriptCurService;

        public ScriptCollectionVM(IUIService ui, IScriptCurrencyService curScript)
        {
            _uiService = ui;
            _scriptCurService = curScript;
            ScriptColl = ScriptCollection.InitTestData();
            Filter = new FilterCommand(this, nameof(SelectedFilter));
            curScript.SelectedScriptChanged += (s, e) =>
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedScript)));
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

        public ScriptType SelectedFilter
        {
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

        public Script? SelectedScript
        {
            get => _scriptCurService.SelectedScript;
            set
            {
                _scriptCurService.SelectedScript = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedScript)));
            }
        }

        #region File Menu
        public ICommand FileNew => new RelayCommand(
            _ => ScriptColl = new ScriptCollection(),
            _ => ScriptColl.Scripts.Count > 0);

        public ICommand FileOpen => new RelayCommand(_ =>
        {
            if (_uiService.OpenFileDialog("XML files (*.xml)|*.xml") is string filePath)
            {
                try
                {
                    using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

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
        #endregion

        #region Edit Menu
        public void ScriptCut_Executed()
        {
            if (SelectedScript is not null)
                ScriptColl.Scripts.Remove(SelectedScript);
        }

        public bool ScriptCut_CanExecute() =>
            SelectedScript is not null; // TODO : Check if a script is selected and can be cut

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
                _parent.PropertyChanged += (s, e) =>
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
            _uiService.ShowError($"Error {action}: {ex.Message}", action);
        }
    }
}
