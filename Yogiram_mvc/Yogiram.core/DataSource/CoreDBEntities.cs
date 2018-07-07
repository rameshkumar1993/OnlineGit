using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogiram.core.Context;
using Yogiram.core.Models;

namespace Yogiram.core.DataSource
{
    public partial class CoreEntities : DbContext
    {
        public CoreEntities(String connectionString)
            : base(connectionString)
        {
            
        }

        public static CoreEntities Create()
        {
            CoreEntities db = new CoreEntities(GetEntityConnectionString());
            return db;

        }

        public static string GetEntityConnectionString()
        {
            string providerName = "System.Data.SqlClient";

            EntityConnectionStringBuilder conStr = new EntityConnectionStringBuilder();
            conStr.Provider = providerName;
            conStr.ProviderConnectionString = ConfigurationManager.ConnectionStrings[Constant.ConnStringName].ToString(); 

            conStr.Metadata = @"res://*/DataSource.CoreEntities.csdl|res://*/DataSource.CoreEntities.ssdl|res://*/DataSource.CoreEntities.msl";
            return conStr.ToString();

        }

    }


}
