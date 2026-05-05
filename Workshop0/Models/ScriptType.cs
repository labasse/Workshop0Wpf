using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Workshop0.Models
{
    public enum ScriptType
    {
        All = 0,
        [XmlEnum(Name = "install"    )] Install,
        [XmlEnum(Name = "check"      )] Check,
        [XmlEnum(Name = "remediation")] Remediation
    }
}
