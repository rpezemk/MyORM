using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLBuilder
{
    public static class Globals
    {
        public static SqlConnection WikiJSConn
        {
            get
            {
                var csb = new SqlConnectionStringBuilder
                {
                    DataSource = @"blah",
                    InitialCatalog = "blah",
                    UserID = "blah",
                    Password = "blah",
                };

                return new SqlConnection(csb.ConnectionString);
            }
        }

        public static SqlConnection XLTestConn
        {
            get
            {
                var csb = new SqlConnectionStringBuilder
                {
                    DataSource = @"blah",
                    InitialCatalog = "blah",
                    UserID = "blah",
                    Password = "blah",
                };

                return new SqlConnection(csb.ConnectionString);
            }
        }


        public static SqlConnection PrzemaHomeConn
        {
            get
            {
                var csb = new SqlConnectionStringBuilder
                {
                    DataSource = @"blah",
                    InitialCatalog = "BikeStores",
                    UserID = "przema",
                    Password = "blah",
                };

                return new SqlConnection(csb.ConnectionString);
            }
        }

    }
}
