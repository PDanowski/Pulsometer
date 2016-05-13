using System;
using Pulsometer.Model.Models.Enums;

namespace Pulsometer.Model.XMLSerialization
{
    public interface IUserConfiguration
    {
        string Name { get; set; }
        DateTime Birthday { get; set; }
        Gender Gender { get; set; }
    }
}
