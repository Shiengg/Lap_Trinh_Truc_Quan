using ChatBox.UserControls;
using ChatGPT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace ChatBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<MyMessageChat> chatMessages = new ObservableCollection<MyMessageChat>();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            messageListView.ItemsSource = chatMessages;
        }

        public class ChatMessage
        {
            public string Time { get; set; }
            public string Sender { get; set; }
            public string Message { get; set; }
        }

        bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1250;
                    this.Height = 830;

                    IsMaximized = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximized = true;
                }
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void Image_MouseUp_1(object sender, MouseButtonEventArgs e)
        {

            Application.Current.Shutdown();
        }

        private void AddMessageToChat(string sender, string message)
        {
            // Tạo một đối tượng ChatMessage
            MyMessageChat chatMessage = new MyMessageChat
            {
 
                Message = message
            };

            // Thêm vào ObservableCollection
            chatMessages.Add(chatMessage);

            
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = txtMessage.Text;
            

            // Thêm tin nhắn người dùng và phản hồi từ GPT vào ListView
            AddMessageToChat("User", userMessage);
            // Xóa nội dung TextBox sau khi gửi
            txtMessage.Text = string.Empty;
        }


        private void MenuButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to log out?", "Confirm logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {

                Login loginWindow = new Login();
                loginWindow.Show();

                if (Application.Current.MainWindow != null)
                {
                    this.Close();
                }
            }
            else
            {

            }
        }

        //private void MenuButton_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
