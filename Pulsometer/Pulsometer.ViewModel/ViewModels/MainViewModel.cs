using System;
using Pulsometer.Model.SQLiteConnection;
using Pulsometer.ViewModel.Interfaces;

namespace Pulsometer.ViewModel.ViewModels
{
    public class MainViewModel
    {
        private readonly IMainViewAccess access;
        private SQLiteConnector connection;

        public MainViewModel(IMainViewAccess access)
        {
            this.access = access;

            connection = new SQLiteConnector();
            connection.CreateTable();
        }

    }
}