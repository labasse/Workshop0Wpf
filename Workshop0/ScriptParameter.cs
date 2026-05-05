using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Workshop0
{
    public class ScriptParameter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [XmlAttribute("name")]
        public required string Name { 
            get; 
            set {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            } 
        }

        [XmlAttribute("value")]
        public required string Value
        {
            get;
            set
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }
    }
}
