using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;

namespace ChatBox.ViewModel
{
    class UserVM: Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public int UserID
        {
            get { return _pageModel.User; }
            set { _pageModel.User = value; OnPropertyChanged(); }
        }

        public UserVM()
        {
            _pageModel = new PageModel();
            UserID = 0;
        }
    }
}
