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
            //messageListView.ItemsSource = chatMessages;
            AttachTextBoxEvents();
        }

        private void AttachTextBoxEvents()
        {
            txtMessage.PreviewKeyDown += txtMessage_PreviewKeyDown;
            txtMessage.PreviewKeyUp += txtMessage_PreviewKeyUp;
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

        private bool isShiftKeyPressed = false;
        //
        private void txtMessage_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                isShiftKeyPressed = true;
            }
            else if (e.Key == Key.Enter)
            {
                if (!isShiftKeyPressed)
                {
                    SendButton_Click(sender, e);
                    e.Handled = true;
                }
            }
        }

        private void txtMessage_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                isShiftKeyPressed = false;
            }
        }

        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = txtMessage.Text;

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

            //string generatedText = await GetGeneratedTextFromAI(userMessage);

            Output newOutput = new Output
            {
                Message = "Test, đang thử giao diện",
            };

            ChatPanel.Children.Add(newOutput);

            // Xóa nội dung TextBox sau khi gửi
            txtMessage.Text = string.Empty;
        }


        private async Task<string> GetGeneratedTextFromAI(string userInput)
        {
            var client = new HttpClient();
            var baseUrl = "https://api.openai.com/v1/chat/completions";
            //API key tạm thời bị khoá rồi, nào xong hết gỡ khỏi github mới thêm key.
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

        private void txtMessage_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            textBox.Height = textBox.ExtentHeight;
        }

        private void txtboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtboxSearch.Text.ToLower(); // Lấy nội dung từ TextBox tìm kiếm và chuyển về chữ thường

            foreach (var child in ChatPanel.Children)
            {
                if (child is UIElement uiElement)
                {
                    // Kiểm tra nội dung của mỗi phần tử trong StackPanel
                    // Ví dụ: giả sử mỗi phần tử là TextBlock và bạn muốn tìm kiếm dựa trên Text của nó
                    if (uiElement is TextBlock textBlock)
                    {
                        if (textBlock.Text.ToLower().Contains(searchText))
                        {
                            // Hiển thị hoặc ẩn phần tử tìm thấy dựa trên kết quả tìm kiếm
                            uiElement.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            uiElement.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtboxSearch.Text;
            HighlightSearchText(searchText);
        }

        private void HighlightSearchText(string searchText)
        {
            foreach (var child in ChatPanel.Children)
            {
                if (child is TextBlock textBlock)
                {
                    string originalText = textBlock.Text;
                    string lowerOriginalText = originalText.ToLower();
                    string lowerSearchText = searchText.ToLower();

                    int index = lowerOriginalText.IndexOf(lowerSearchText);
                    if (index >= 0)
                    {
                        var run = new Run(originalText);
                        run.Foreground = Brushes.Black; // Màu chữ mặc định

                        var highlight = new TextRange(run.ContentStart.GetPositionAtOffset(index), run.ContentStart.GetPositionAtOffset(index + searchText.Length));
                        highlight.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Yellow); // Bôi màu nền

                        textBlock.Inlines.Clear();
                        textBlock.Inlines.Add(run);
                    }
                }
            }
        }


        //private void MenuButton_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}
    }
}
