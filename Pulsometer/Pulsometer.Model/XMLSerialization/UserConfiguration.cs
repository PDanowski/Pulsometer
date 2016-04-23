using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Pulsometer.Model.XMLSerialization;

namespace Pulsometer.Model.Models
{
    public enum Gender
    {
        Mężczyzna,
        Kobieta
    }

    [Serializable()]
    public class UserConfiguration : IUserConfiguration
    {
        [System.Xml.Serialization.XmlElement("Name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElement("Age")]
        public int Age { get; set; }

        [System.Xml.Serialization.XmlElement("Gender")]
        public Gender Gender { get; set; }

        [System.Xml.Serialization.XmlElement("Notifications")]
        private List<DateTime> notifications;
        public List<DateTime> Notifications
        {
            get
            {
                if (notifications == null)
                    notifications = new List<DateTime>();
                return notifications;
            }

            set
            {
                notifications = value;
            }
        }
    }
}
