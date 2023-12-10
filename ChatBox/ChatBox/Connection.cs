using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace ChatBox
{
    class Connection
    {
        private static string stringConnection= @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Năm 2\ChatBox\Lap_Trinh_Truc_Quan\ChatBox\ChatBox\Database1.mdf"";Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }

    }
}
