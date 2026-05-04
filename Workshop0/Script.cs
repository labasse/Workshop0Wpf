using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Workshop0
{
    public class Script
    {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("type")]
        public ScriptType Type { get; set; }

        [XmlAttribute("path")]
        public required string Path { get; set; }
    }
}
