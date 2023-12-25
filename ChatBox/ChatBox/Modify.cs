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

        private static string loggedInUserEmail; // Biến global để lưu trữ email của người dùng đã đăng nhập

        public static void SetLoggedInUserEmail(string email)
        {
            loggedInUserEmail = email;
        }

        public static string GetLoggedInUserEmail()
        {
            return loggedInUserEmail;
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

    }
}
