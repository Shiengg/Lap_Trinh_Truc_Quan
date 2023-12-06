using ChatBox.UserControls;
using ChatGPT;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = txtMessage.Text;

            if (userMessage.Length == 0 || userMessage == " ")
            {

            }
            else
            {
                Input newInput = new Input
                {
                    Message = userMessage,
                };
                ChatPanel.Children.Add(newInput);

                //string generatedText = await GetGeneratedTextFromAI(userMessage);

                Output newOutput = new Output
                {
                    Message = "Test, đang thử giao diện",
                };

                ChatPanel.Children.Add(newOutput);

                // Xóa nội dung TextBox sau khi gửi
                txtMessage.Text = string.Empty;
            }

        }


        private async Task<string> GetGeneratedTextFromAI(string userInput)
        {
            var client = new HttpClient();
            var baseUrl = "https://api.openai.com/v1/chat/completions";
            //API key tạm thời bị khoá rồi, nào xong hết gỡ khỏi github mới thêm key đư
            var apiKey = "YOUR_API_KEY";
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            while (true)
            {

                var parameters = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[] { new { role = "user", content = userInput }, },
                    max_tokens = 1024,
                    temperature = 0.2f,
                };

                var response = await client.PostAsync(baseUrl, new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json"));// new StringContent(json));

                // Read the response
                var responseContent = await response.Content.ReadAsStringAsync();

                // Extract the completed text from the response
                dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                string generatedText = responseObject.choices[0].message.content;

                return generatedText;
            }
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
