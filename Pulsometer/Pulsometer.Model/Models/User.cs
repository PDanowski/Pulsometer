using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Pulsometer.Model.Models
{
    public static class User
    {
        public static string Name { get; set; }
        public static int Age { get; set; }
        public static List<DateTime> Notifications { get; set; }

    }
}
