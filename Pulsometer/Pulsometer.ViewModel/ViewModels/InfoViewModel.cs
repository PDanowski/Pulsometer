using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pulsometer.Services;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class InfoViewModel
    {
        private readonly IInfoViewAccess access;

        public InfoViewModel(IInfoViewAccess access)
        {
            this.access = access;
        }

        public string LoadHtml(Stream str)
        {
            return FileTools.LoadStream(str);
        }
    }
}
