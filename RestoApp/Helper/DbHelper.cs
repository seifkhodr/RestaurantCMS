using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace RestoApp.Helper
{
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(
                ConfigurationManager
                .ConnectionStrings["RestoDbConnection"]
                .ConnectionString
            );
        }
    }
}
