using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatBox.View
{
    /// <summary>
    /// Interaction logic for Page3.xaml
    /// </summary>
    public partial class Page3 : UserControl
    {
        public Page3()
        {
            InitializeComponent();
        }

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
            Descript.Text = result;
        }
    }

}
