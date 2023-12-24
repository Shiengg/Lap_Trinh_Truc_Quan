using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;

namespace ChatBox.ViewModel
{
    class CalenderVM: Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public DateOnly CalenderDisplay
        {
            get { return _pageModel.Calender; }
            set { _pageModel.Calender = value; OnPropertyChanged(); }
        }

        public CalenderVM()
        {
            _pageModel = new PageModel();

        }
    }
}
