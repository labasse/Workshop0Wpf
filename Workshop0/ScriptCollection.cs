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
        public void InitTestData()
        {
            Path = @"C:\Scripts\scripts.xml";
            Scripts.Clear();
            Scripts.Add(new Script { Name = "Install Chocolatey", Type = ScriptType.Install    , Path = @"C:\Scripts\install_chocolatey.ps1" });
            Scripts.Add(new Script { Name = "Check Chocolatey"  , Type = ScriptType.Check      , Path = @"C:\Scripts\check_chocolatey.ps1" });
            Scripts.Add(new Script { Name = "Remediate Chocolatey", Type = ScriptType.Remediation, Path = @"C:\Scripts\remediate_chocolatey.ps1" });
            Scripts.Add(new Script { Name = "Install Git"       , Type = ScriptType.Install    , Path = @"C:\Scripts\install_git.ps1" });
            Scripts.Add(new Script { Name = "Check Git"         , Type = ScriptType.Check      , Path = @"C:\Scripts\install_git.ps1" });
            Scripts.Add(new Script { Name = "Remediate Git"     , Type = ScriptType.Remediation, Path = @"C:\Scripts\install_git.ps1" });
        }

        public static ScriptCollection? Load(Stream s)
        {
            var serializer = new XmlSerializer(typeof(ScriptCollection));

            return (ScriptCollection?)serializer.Deserialize(s);
        }

        public ObservableCollection<Script> Scripts { get; set; } = new ObservableCollection<Script>();

        public required string Path { get; set; }


        public IEnumerable<object> Types         {
            get => Scripts
                .GroupBy(s => s.Type)
                .Select(group => new { 
                    ScriptType = group.Key, 
                    Count = group.Count() 
                });
        }
    }
}
