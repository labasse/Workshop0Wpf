using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
                if(value.Length is < 2 or > 20)
                {
                    throw new ValidationException("Le nom doit être entre 2 and 20 caractères compris.");
                }
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
                   throw new ValidationException("Format de chemin Windows non valide.");
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
