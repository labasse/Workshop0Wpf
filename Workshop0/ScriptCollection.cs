using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Workshop0
{
    [XmlRoot("scripts")]
    public class ScriptCollection
    {
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

        public string Path { get; set; } = string.Empty;
    }
}
