using System;
using Pulsometer.Model.Models.Enums;

namespace Pulsometer.Model.XMLSerialization
{
    [Serializable()]
    public class UserConfiguration : IUserConfiguration
    {
        [System.Xml.Serialization.XmlElement("Name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("Age")]
        public DateTime Birthday { get; set; }

        [System.Xml.Serialization.XmlElement("Gender")]
        public Gender Gender { get; set; }
    }
}
