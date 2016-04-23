using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pulsometer.Model.Models;
using Pulsometer.Model.XMLSerialization;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class SettingsViewModel
    {
        private readonly ISettingsViewAccess access;
        private IUserConfiguration config;

        public SettingsViewModel(ISettingsViewAccess access, IUserConfiguration config)
        {
            this.access = access;
            this.config = config;
        }

    }
}
