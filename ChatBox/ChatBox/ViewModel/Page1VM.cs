using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;

namespace ChatBox.ViewModel
{
    class Page1VM: Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public string PageOne
        {
            get { return _pageModel.Page1; }
            set { _pageModel.Page1 = value;OnPropertyChanged(); }
        }
        public Page1VM()
        {
            _pageModel = new PageModel();
        }
    }
}
