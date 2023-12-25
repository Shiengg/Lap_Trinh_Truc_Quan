using ChatBox.View.Components;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChatBox.View
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : UserControl
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void MicButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InputImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private bool userScrolled = false;
        private DispatcherTimer scrollTimer;

        private void ChatScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            // Tự động cuộn xuống dưới cùng khi Loaded
            ScrollToBottom();
        }

        private void ChatScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            // Kiểm tra xem người dùng đã cuộn chưa
            if (!userScrolled)
            {
                // Tự động cuộn xuống dưới cùng
                ScrollToBottom();
            }

            // Đặt lại giá trị userScrolled sau một khoảng thời gian
            ResetUserScrolledTimer();
        }

        // Hàm để tự động cuộn xuống dưới cùng
        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToEnd();
        }

        // Hàm xử lý sự kiện PreviewMouseWheel để đánh dấu là người dùng đã cuộn
        private void ChatScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            userScrolled = true;

            // Đặt lại giá trị userScrolled sau một khoảng thời gian
            ResetUserScrolledTimer();
        }

        // Đặt lại giá trị userScrolled sau một khoảng thời gian
        private void ResetUserScrolledTimer()
        {
            if (scrollTimer == null)
            {
                scrollTimer = new DispatcherTimer();
                scrollTimer.Interval = TimeSpan.FromSeconds(0.2); // Đặt thời gian 
                scrollTimer.Tick += (sender, args) =>
                {
                    userScrolled = false;
                    scrollTimer.Stop();
                };
            }

            scrollTimer.Stop();
            scrollTimer.Start();
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = txtMessage.Text.Trim(); // Loại bỏ các khoảng trắng ở đầu và cuối chuỗi

            // Kiểm tra xem chuỗi có chứa chỉ ký tự trắng hoặc xuống dòng không
            if (string.IsNullOrWhiteSpace(userMessage))
            {
                // Ngăn chặn việc thêm Input nếu dữ liệu người dùng trống
                return;
            }

            Input newInput = new Input
            {
                Message = userMessage,
            };
            ChatPanel.Children.Add(newInput);

            string result = await CallApi(userMessage);

            Output newOutput = new Output
            {
                Message = result,
            };
            ChatPanel.Children.Add(newOutput);

            // Xóa nội dung TextBox sau khi gửi
            txtMessage.Text = string.Empty;
        }

        private async Task<string> CallApi(string userInput)
        {
            string apiKey = "AIzaSyBcy3pZDBaXFpRSw43W7t9xYxrUsHX0Zdo";
            string apiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=" + apiKey;

            string jsonData = $"{{\"contents\":[{{\"parts\":[{{\"text\":\"{userInput}\"}}]}}]}}";

            using (HttpClient client = new HttpClient()) 
            {
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject jsonResult = JObject.Parse(result);
                    JArray candidates = (JArray)jsonResult["candidates"];

                    foreach (var candidate in candidates)
                    {
                        string text = (string)candidate["content"]["parts"][0]["text"];
                        return text; // Trả về kết quả từ API
                    }
                }
                else
                {
                    MessageBox.Show("Error: " + response.StatusCode);
                }    
            }
            return "Some problem about connect!!!!";
        }
    }
}