using ChatBox.UserControls;
using ChatBox.Utilities;
using ChatBox.ViewModel;
using ChatGPT;
using Microsoft.Win32;
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
using System.Windows.Threading;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;



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
            DataContext = new NavigationVM();
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

            //string generatedText = await GetGeneratedTextFromAI(userMessage);

            Output newOutput = new Output
            {
                Message = "Test, đang thử giao diện",
            };

            ChatPanel.Children.Add(newOutput);

            // Xóa nội dung TextBox sau khi gửi
            txtMessage.Text = string.Empty;
        }

        private bool IsWhiteSpaceOrNewLine(string text)
        {
            foreach (char c in text)
            {
                if (!char.IsWhiteSpace(c))
                {
                    return false; // Nếu có ít nhất một ký tự không phải là khoảng trắng hoặc xuống dòng
                }
            }
            return true; // Nếu chuỗi chỉ chứa ký tự trắng hoặc xuống dòng
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

        private void MenuButton_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MicButton_Click(object sender, RoutedEventArgs e)
        {

        }


        //private void MenuButton_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        //Nhận diện hình ảnh, chỉ đơn giản cho ra 1 cái mô tả cho hình ảnh truyền vào ;-;
        public class ImageRecognitionService
        {
            private readonly string apiKey = "4064be77337747769d9f837f88afe9e6";
            private readonly string endpoint = "https://apiforchatbot.cognitiveservices.azure.com/";

            public async Task<string> RecognizeImageAsync(string imagePath)
            {
                var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(apiKey))
                {
                    Endpoint = endpoint
                };

                using (var imageStream = File.OpenRead(imagePath))
                {
                    var result = await client.AnalyzeImageInStreamAsync(imageStream, new List<VisualFeatureTypes?>
            {
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Tags
            }.ToList());

                    // Xử lý kết quả ở đây
                    return ParseImageRecognitionResult(result);
                }
            }

            private string ParseImageRecognitionResult(ImageAnalysis result)
            {
                return $" Description: {result.Description.Captions.FirstOrDefault()?.Text}";
            }

        }
        private void InputImage_Click(object sender, RoutedEventArgs e)
        {
            // Mở hộp thoại chọn tệp ảnh từ file
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Lấy đường dẫn của tệp ảnh đã chọn
                string imagePath = openFileDialog.FileName;
                // Hiển thị ảnh trong Image control
                DisplaySelectedImage(imagePath);

                // Gọi phương thức nhận diện ảnh
                RecognizeImage(imagePath);

            }


        }
        private void DisplaySelectedImage(string imagePath)
        {
            // Hiển thị ảnh trong Image control
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath);
            bitmap.EndInit();
            selectedImage.Source = bitmap;
        }

        private async void RecognizeImage(string imagePath)
        {
            ImageRecognitionService imageRecognitionService = new ImageRecognitionService();
            // Gọi phương thức nhận diện ảnh từ ImageRecognitionService
            string result = await imageRecognitionService.RecognizeImageAsync(imagePath);

            // Hiển thị kết quả trong cửa sổ đầu ra hoặc UI của bạn
            MessageBox.Show(result, "Kết Quả Nhận Diện Hình Ảnh");
        }

        // Tự động cuộn xuống để xem tin nhắn mới nhất 

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
    }
}
