using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Workshop0.Models
{
    public partial class Script : INotifyPropertyChanged
    {
        [XmlAttribute("name")]
        public string Name { 
            get => field; 
            set {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            } 
        } = string.Empty;

        [XmlAttribute("type")]
        public ScriptType Type
        {
            get => field;
            set
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Type)));
            }
        }

        [GeneratedRegex(@"^[a-zA-Z]:\\(?:[^\\\/:*?""<>|\r\n]+\\)*[^\\\/:*?""<>|\r\n]*$")]
        private static partial Regex ValidWindowsPathRegex { get; }

        [XmlAttribute("path")]
        public required string Path
        {
            get => field;
            set
            {
                if(!ValidWindowsPathRegex.IsMatch(value))
                {
                   throw new ArgumentException("Invalid Windows path format.", nameof(Path));
                }
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
            }
        }

        [XmlElement("parameter")]
        public ObservableCollection<ScriptParameter> Parameters { get; } = new ObservableCollection<ScriptParameter>();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
