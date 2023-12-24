using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Model;

namespace ChatBox.ViewModel
{
    class DashboardVM : Ultilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        
       public string Dashboard
       {
            get { return _pageModel.Dashboard; }
            set { _pageModel.Dashboard = value; OnPropertyChanged(); }
       }

        public DashboardVM()
        {
            _pageModel = new PageModel();
        }
    }
}
