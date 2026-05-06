using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Workshop0.Models
{
    [XmlRoot("scripts")]
    public partial class ScriptCollection : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public static ScriptCollection InitTestData()
        {
            var res = new ScriptCollection() { Path = @"C:\Scripts\scripts.xml" };
            var script = new Script { Name = "Install Chocolatey", Type = ScriptType.Install, Path = @"C:\Scripts\install_chocolatey.ps1" };

            script.Parameters.Add(new ScriptParameter { Name = "Version", Value = "2.1.0" });
            script.Parameters.Add(new ScriptParameter { Name = "InstallDir", Value = @"C:\Chocolatey" });
            res.Scripts.Add(script);

            script = new Script { Name = "Check Chocolatey", Type = ScriptType.Check, Path = @"C:\Scripts\check_chocolatey.ps1" };
            script.Parameters.Add(new ScriptParameter { Name = "Version", Value = "1.0.0" });
            script.Parameters.Add(new ScriptParameter { Name = "InstallDir", Value = @"C:\Chocolatey\" });
            res.Scripts.Add(script);

            res.Scripts.Add(new Script { Name = "Remediate Chocolatey", Type = ScriptType.Remediation, Path = @"C:\Scripts\remediate_chocolatey.ps1" });
            res.Scripts.Add(new Script { Name = "Install Git"       , Type = ScriptType.Install    , Path = @"C:\Scripts\install_git.ps1" });
            res.Scripts.Add(new Script { Name = "Check Git"         , Type = ScriptType.Check      , Path = @"C:\Scripts\install_git.ps1" });
            res.Scripts.Add(new Script { Name = "Remediate Git"     , Type = ScriptType.Remediation, Path = @"C:\Scripts\install_git.ps1" });
            return res;
        }

        public static ScriptCollection Load(Stream s)
        {
            var serializer = new XmlSerializer(typeof(ScriptCollection));

            return (ScriptCollection)(
                serializer.Deserialize(s) 
                ?? throw new NullReferenceException("Unexpected null result")
            );
        }

        [XmlElement("script")]
        public ObservableCollection<Script> Scripts { get; } = new ObservableCollection<Script>();

        [GeneratedRegex(@"^[a-zA-Z]:\\(?:[^\\\/:*?""<>|\r\n]+\\)*[^\\\/:*?""<>|\r\n]*$")]
        private static partial Regex ValidWindowsPathRegex { get; }

        public string Path { 
            get;
            set
            {
                if (!ValidWindowsPathRegex.IsMatch(value))
                {
                    throw new ArgumentException("Invalid Windows path format.", nameof(Path));
                }
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Path)));
            }
        } = string.Empty;
    }
}
