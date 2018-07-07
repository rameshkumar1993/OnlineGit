using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Web;

namespace Manage.DataSource
{
    public partial class ManageEntities : DbContext
    {
        public ManageEntities(String connectionString)
            : base(GetEntityConnectionString())
        {

        }

        public static string GetEntityConnectionString()
        {
            string providerName = "System.Data.SqlClient"; ///MS-SQL server was the database            /// server

            EntityConnectionStringBuilder conStr = new EntityConnectionStringBuilder();
            conStr.Provider = providerName;
            conStr.ProviderConnectionString = ConfigurationManager.ConnectionStrings["ConnStrAt"].ToString(); //database connection string

            //main assembly level configuration
            conStr.Metadata = @"res://*/DataSource.ManageEntities.csdl|res://*/DataSource.ManageEntities.ssdl|res://*/DataSource.ManageEntities.msl";
            return conStr.ToString();

        }

    }
}