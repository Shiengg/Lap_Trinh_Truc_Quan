using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;

namespace ChatBox.ViewModel
{
    class UserVM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        //public int UserID
        //{
        //    get { return _pageModel.User; }
        //    set { _pageModel.User = value; OnPropertyChanged(); }
        //}

        //public UserVM()
        //{
        //    _pageModel = new PageModel();
        //    UserID = 0;
        //}
        public string User
        {
            get { return _pageModel.User; }
            set
            {
                if (_pageModel.User != value)
                {
                    _pageModel.User = value;
                    OnPropertyChanged("User");
                }
            }
        }

        public string Birthday
        {
            get { return _pageModel.Birthday; }
            set
            {
                if (_pageModel.Birthday != value)
                {
                    _pageModel.Birthday = value;
                    OnPropertyChanged("Birthday");
                }
            }
        }

        public string Introduce
        {
            get { return _pageModel.Introduce; }
            set
            {
                if (_pageModel.Introduce != value)
                {
                    _pageModel.Introduce = value;
                    OnPropertyChanged("Introduce");
                }
            }
        }

        public UserVM()
        {
            _pageModel = new PageModel();
            User = "lmao";
            Birthday = ""; // giá trị mặc định cho birthday
            Introduce = ""; // giá trị mặc định cho introduce

        }

        public async void LoadUserData(string userEmail)
        {
            // Lấy thông tin từ MongoDB
            //var userInfo = await Connection.GetUserInfoFromMongoDB(userEmail);

            //// Gán thông tin vào UserVM
            //_pageModel.User = userInfo.User;
            //_pageModel.Birthday = userInfo.Birthday;
            //_pageModel.Introduce = userInfo.Introduce;
            var userInfo = await Connection.GetUserInfoFromMongoDB(userEmail);

            if (userInfo != null)
            {
                _pageModel.User = userInfo.User ?? ""; // Kiểm tra và gán giá trị hoặc rỗng nếu null
                _pageModel.Birthday = userInfo.Birthday ?? "01/01/2000"; // Sử dụng giá trị mặc định nếu null
                _pageModel.Introduce = userInfo.Introduce ?? "Hello!"; // Sử dụng giá trị mặc định nếu null
            }
            else
            {
                // Xử lý khi không tìm thấy thông tin người dùng trong MongoDB dựa trên email đã cung cấp
                // Ví dụ: thông báo lỗi, đặt giá trị mặc định, hoặc thực hiện hành động khác tùy thuộc vào logic của ứng dụng
            }
        }

    }
}