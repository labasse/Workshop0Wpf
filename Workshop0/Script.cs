using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Workshop0
{
    public class Script : INotifyPropertyChanged
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

        [XmlAttribute("path")]
        public required string Path
        {
            get => field;
            set
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
