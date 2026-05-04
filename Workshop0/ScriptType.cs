using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Workshop0
{
    public enum ScriptType
    {
        [XmlEnum(Name = "install"    )] Install,
        [XmlEnum(Name = "check"      )] Check,
        [XmlEnum(Name = "remediation")] Remediation
    }
}
