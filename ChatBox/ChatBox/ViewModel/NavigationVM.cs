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
        public ICommand GPTChatCommand { get; set; }
        public ICommand UserCommand { get; set; }
        public ICommand CalendarCommand { get; set; }
        public ICommand ImageRecognitionCommand { get; set; }
        public ICommand GeminiChatCommand { get; set; }
        public ICommand Page3Command { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void GPTChat(object obj) => CurrentView = new GPTChatVM();
        private void User(object obj) => CurrentView = new UserVM();
        private void Calendar(object obj) => CurrentView = new CalendarVM();
        private void ImageRecognition(object obj) => CurrentView = new ImageRecpgnitionVM();
        private void GeminiChat(object obj) => CurrentView = new GeminiChatVM();
        
        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            GPTChatCommand = new RelayCommand(GPTChat);
            UserCommand = new RelayCommand(User);
            CalendarCommand = new RelayCommand(Calendar);
            ImageRecognitionCommand = new RelayCommand(ImageRecognition);
            GeminiChatCommand = new RelayCommand(GeminiChat);
            

            CurrentView= new HomeVM();
        }
    }
}
