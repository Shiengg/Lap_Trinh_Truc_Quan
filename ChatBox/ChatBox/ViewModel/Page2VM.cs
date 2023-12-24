using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;

namespace ChatBox.ViewModel
{
    class Page2VM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string PageTwo
        {
            get { return _pageModel.Page2; }
            set { _pageModel.Page2 = value; OnPropertyChanged(); }
        }
        public Page2VM()
        {
            _pageModel = new PageModel();
        }
    }
}
