using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ChatBox
{
    class Modify
    {
        public Modify() 
        {
        }
        SqlCommand sqlCommand;//truy vấn các câu lệnh insert,update,delete,...
        SqlDataReader dataReader; //đọc dữ liệu trong bảng
        //public List<Account> Accounts(string query)
        //{
        //    List<Account> accounts = new List<Account>();
        //    using (SqlConnection sqlConnection = Connection.GetSqlConnection())
        //    {
        //        sqlConnection.Open();
        //        sqlCommand=new SqlCommand(query,sqlConnection);
        //        dataReader= sqlCommand.ExecuteReader();
        //        while (dataReader.Read()) 
        //        {
        //            accounts.Add(new Account(dataReader.GetString(0), dataReader.GetString(1)));
        //        }
        //        sqlConnection.Close();
        //    }
        //    return accounts; 
        //}

        public List<Account> GetAccounts(string query, string email, string password)//Check account
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Admin\\Documents\\Zalo Received Files\\ChatBox\\ChatBox\\ChatBox\\Database1.mdf\";Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // Assuming Account is a class representing your user account data
                        Account account = new Account
                        {
                            // Populate account properties from reader columns
                        };
                        accounts.Add(account);
                    }

                    reader.Close();
                }
            }

            return accounts;
        }

        //public void Commad(string query)
        //{
        //    using (SqlConnection sqlConnection = Connection.GetSqlConnection())
        //    {
        //        sqlConnection.Open();
        //        sqlCommand = new SqlCommand(query, sqlConnection);
        //        sqlCommand.ExecuteNonQuery();//thực thi câu truy vấn
        //        sqlConnection.Close();
        //    }
        //}

        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Admin\\Documents\\Zalo Received Files\\ChatBox\\ChatBox\\ChatBox\\Database1.mdf\";Integrated Security=True"; // Replace with your connection string

        // Method to count existing accounts with a specific email
        public int CountAccounts(string query, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count;
                }
            }
        }

        // Method to insert account details into the database
        public void InsertAccount(string query, string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Pass", password);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        // Define the asynchronous method CountAccountsAsync

        public async Task<int> CountAccountsAsync(string query, string email)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = query; // query để đếm tài khoản dựa trên email

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Email", email); // Sử dụng tham số để tránh SQL injection

                await connection.OpenAsync();

                // ExecuteScalarAsync trả về số lượng tài khoản
                object result = await command.ExecuteScalarAsync();

                int count = Convert.ToInt32(result); // Chuyển đổi kết quả về số nguyên

                return count;
            }
        }



    }
}
