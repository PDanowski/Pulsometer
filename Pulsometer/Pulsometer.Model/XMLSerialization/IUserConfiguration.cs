using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Pulsometer.Model.Models;

namespace Pulsometer.Model.XMLSerialization
{
    public interface IUserConfiguration
    {
        string Name { get; set; }
        int Age { get; set; }
        Gender Gender { get; set; }
        List<DateTime> Notifications { get; set; }
    }
}
