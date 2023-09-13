using HospitalManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HospitalManagementSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User? LoggedInUser;
        public static HospitalDBContext DBContext = new HospitalDBContext();
        public static SqlConnection? DBConnection;

        public App()
        {
            string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            if (connectionString != null)
            {
                DBConnection = new SqlConnection(connectionString);
            }
        }
    }
}
