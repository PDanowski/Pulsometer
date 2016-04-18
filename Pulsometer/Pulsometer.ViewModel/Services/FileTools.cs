using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pulsometer.Services
{
    public static class FileTools
    {
        public static string LoadStream(Stream file)
        {
            String content = "";
            using (StreamReader stream = new StreamReader(file))
            {
                content = stream.ReadToEnd();
            }
            return content;
        }
    }
}