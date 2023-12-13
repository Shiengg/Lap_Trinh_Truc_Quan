using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatBox.Utilities;
using System.Windows.Input;
using System.Transactions;

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
        public ICommand UserCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void User(object obj) => CurrentView = new UserVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            UserCommand = new RelayCommand(User);

            // Startup Page
            CurrentView = new HomeVM();
        }

    }
}
