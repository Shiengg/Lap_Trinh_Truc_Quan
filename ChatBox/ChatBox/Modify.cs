using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace ChatBox
{
    class Modify
    {
        //    public Modify() 
        //    {
        //    }
        //    SqlCommand sqlCommand;//truy vấn các câu lệnh insert,update,delete,...
        //    SqlDataReader dataReader; //đọc dữ liệu trong bảng

        //    public List<Account> GetAccounts(string query, string email, string password)//Check account
        //    {
        //        List<Account> accounts = new List<Account>();

        //        using (SqlConnection connection = new SqlConnection(@"mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/Chatbox?retryWrites=true&w=majority"))
        //        {
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@Email", email);
        //                command.Parameters.AddWithValue("@Password", password);

        //                connection.Open();

        //                SqlDataReader reader = command.ExecuteReader();

        //                while (reader.Read())
        //                {
        //                    // Assuming Account is a class representing your user account data
        //                    Account account = new Account
        //                    {
        //                        // Populate account properties from reader columns
        //                    };
        //                    accounts.Add(account);
        //                }

        //                reader.Close();
        //            }
        //        }

        //        return accounts;
        //    }

        //    private string connectionString = @"mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/Chatbox?retryWrites=true&w=majority";

        //    // Method to count existing accounts with a specific email
        //    public int CountAccounts(string query, string email)
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@Email", email);
        //                connection.Open();
        //                int count = (int)command.ExecuteScalar();
        //                return count;
        //            }
        //        }
        //    }

        //    // Method to insert account details into the database
        //    public void InsertAccount(string query, string email, string password)
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                command.Parameters.AddWithValue("@Email", email);
        //                command.Parameters.AddWithValue("@Pass", password);
        //                connection.Open();
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //    }

        //    // Define the asynchronous method CountAccountsAsync

        //    public async Task<int> CountAccountsAsync(string query, string email)
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            string sql = query; // query để đếm tài khoản dựa trên email

        //            SqlCommand command = new SqlCommand(sql, connection);
        //            command.Parameters.AddWithValue("@Email", email); // Sử dụng tham số để tránh SQL injection

        //            await connection.OpenAsync();

        //            // ExecuteScalarAsync trả về số lượng tài khoản
        //            object result = await command.ExecuteScalarAsync();

        //            int count = Convert.ToInt32(result); // Chuyển đổi kết quả về số nguyên

        //            return count;
        //        }
        //    }

        private IMongoCollection<Account> accountCollection;

        public List<Account> GetAccounts(string email)
        {
            var filter = Builders<Account>.Filter.Eq(x => x.Email, email);
            var accounts = accountCollection.Find(filter).ToList();
            return accounts;
        }



        public long CountAccounts(string email)
        {
            var filter = Builders<Account>.Filter.Eq("Email", email);
            long count = accountCollection.CountDocuments(filter);
            return count;
        }

        public void InsertAccount(string email, string password)
        {
            var newAccount = new Account { Email = email, Password = password }; // Tạo đối tượng Account mới

            accountCollection.InsertOne(newAccount); // Thêm đối tượng Account vào collection trong MongoDB
        }

        public async Task<long> CountAccountsAsync(string email)
        {
            var filter = Builders<Account>.Filter.Eq("Email", email);
            var count = await accountCollection.CountDocumentsAsync(filter);
            return count;
        }

    }
}
