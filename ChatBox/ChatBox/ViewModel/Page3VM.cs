using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;

namespace ChatBox.ViewModel
{
    class Page3VM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string PageThree
        {
            get { return _pageModel.Page3; }
            set { _pageModel.Page3 = value; OnPropertyChanged(); }
        }
        public Page3VM()
        {
            _pageModel = new PageModel();
        }
    }
}
