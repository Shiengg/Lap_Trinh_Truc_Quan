using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Ultilities;
using System.Windows.Input;
using System.Security.RightsManagement;

namespace ChatBox.ViewModel
{
    class NavigationVM: ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand DashboardCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand CalenderCommand { get; set; }
        public ICommand Page1Command { get; set; }
        public ICommand Page2Command { get; set; }
        public ICommand Page3Command { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Dashboard(object obj) => CurrentView = new DashboardVM();
        private void User(object obj) => CurrentView = new UserVM();
        private void Calender(object obj) => CurrentView = new CalenderVM();
        private void Page1(object obj) => CurrentView = new Page1VM();
        private void Page2(object obj) => CurrentView = new Page2VM();
        private void Page3(object obj) => CurrentView = new Page3VM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            DashboardCommand = new RelayCommand(Dashboard);
            UserCommand = new RelayCommand(User);
            CalenderCommand = new RelayCommand(Calender);
            Page1Command = new RelayCommand(Page1);
            Page2Command = new RelayCommand(Page2);
            Page3Command = new RelayCommand(Page3);

            CurrentView= new HomeVM();
        }
    }
}
