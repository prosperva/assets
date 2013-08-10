using System;
using System.Data.SqlClient;
using System.Configuration;

namespace SAAssetManagement.Reports.Connections
{
    public class Connections
    {
        public SqlConnection MainConnection()
        {
            SqlConnection myConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["AssetContext"].ConnectionString);
            return myConnection;
        }
    }
}