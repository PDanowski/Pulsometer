using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Pulsometer.Model.Models;

namespace Pulsometer.Model.XMLSerialization
{
    public static class UserSerializer
    {
        public static void Serialize(IUserConfiguration config)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserConfiguration));
            using (XmlWriter writer = XmlWriter.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "user.xml")))
            {
                serializer.Serialize(writer, config);
            }
        }

        public static IUserConfiguration Deserialize()
        {
            IUserConfiguration config = null;

            XmlSerializer serializer = new XmlSerializer(typeof(UserConfiguration));
            StreamReader reader = null;

            try
            {
                reader = new StreamReader(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "user.xml"));
            }
            catch (Exception)
            {
                return null;
            }
            config = (UserConfiguration)serializer.Deserialize(reader);
            reader.Close();

            return config;
        }
    }
}
