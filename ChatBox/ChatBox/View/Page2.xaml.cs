using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

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

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
