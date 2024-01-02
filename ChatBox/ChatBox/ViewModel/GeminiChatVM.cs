using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;

namespace ChatBox.ViewModel
{
    class GeminiChatVM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string PageTwo
        {
            get { return _pageModel.GeminiChat; }
            set { _pageModel.GeminiChat = value; OnPropertyChanged(); }
        }
        public GeminiChatVM()
        {
            _pageModel = new PageModel();
        }
    }
}
