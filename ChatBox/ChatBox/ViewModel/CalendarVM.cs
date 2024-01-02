using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;

namespace ChatBox.ViewModel
{
    class CalendarVM: Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public DateOnly CalenderDisplay
        {
            get { return _pageModel.Calendar; }
            set { _pageModel.Calendar = value; OnPropertyChanged(); }
        }

        public CalendarVM()
        {
            _pageModel = new PageModel();

        }
    }
}
