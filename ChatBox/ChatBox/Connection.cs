using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using MongoDB.Driver;
using ChatBox;

namespace ChatBox
{
    class Connection
    {
        //private static string stringConnection= @"mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/Chatbox?retryWrites=true&w=majority";
        private IMongoDatabase database;
        string databaseName = "QLChatbox";
        string collectionName = "Account";
        string connectionString = "mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/QLChatbox?retryWrites=true&w=majority";
        public Connection(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            if (client != null)
            {
                database = client.GetDatabase(databaseName);
                accountCollection = database.GetCollection<Account>(collectionName); // Thay "AccountCollectionName" bằng tên collection thực tế trong cơ sở dữ liệu của bạn
            }

        }

        private IMongoCollection<Account> accountCollection;
        public async Task<long> CountAccountsAsync(string email)
        {
            var filter = Builders<Account>.Filter.Eq("Email", email);
            long count = await accountCollection.CountDocumentsAsync(filter);
            return count;
        }
        public void InsertAccount(string email, string password)
        {
            var newAccount = new Account { Email = email, Password = password };
            accountCollection.InsertOne(newAccount);
        }

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

        private static string loggedInUserEmail; // Biến global để lưu trữ email của người dùng đã đăng nhập

        private static string loggedInUserPassword;
        public static void SetLoggedInUserEmail(string email)
        {
            loggedInUserEmail = email;
        }
        public static void SetLoggedInUserPass(string pass)
        {
            loggedInUserPassword = pass;
        }

        public static string GetLoggedInUserEmail()
        {
            return loggedInUserEmail;
        }
        public static string GetLoggedInUserPass()
        {
            return loggedInUserPassword;
        }

        public void UpdateUserInformation(string userBirthday, string userIntroduce)
        {
            string userEmail = GetLoggedInUserEmail(); // Lấy email của người dùng đã đăng nhập

            if (string.IsNullOrEmpty(userEmail))
            {
                // Xử lý trường hợp người dùng chưa đăng nhập hoặc có lỗi trong việc lấy email đăng nhập
                return;
            }

            var filter = Builders<Account>.Filter.Eq(x => x.Email, userEmail);
            var update = Builders<Account>.Update
                .Set(x => x.Birthday, userBirthday)
                .Set(x => x.Introduce, userIntroduce);

            var result = accountCollection.UpdateOne(filter, update);

            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                // Thành công: thông tin đã được cập nhật vào tài khoản người dùng
            }
            else
            {
                // Xảy ra lỗi: không thể cập nhật thông tin, cần xử lý tình huống này
            }


        }
        public void InsertAccountInfo(string email, string password, string user, string birthday, string introduce)
        {
            var newAccount = new Account
            {
                Email = email,
                Password = password,
                User = user,
                Birthday = birthday,
                Introduce = introduce
            };
            accountCollection.InsertOne(newAccount);
        }


    }
}
