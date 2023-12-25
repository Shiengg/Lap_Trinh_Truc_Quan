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

namespace ChatBox.View
{
    /// <summary>
    /// Interaction logic for User.xaml
    /// </summary>
    public partial class User : UserControl
    {
        public User()
        {
            InitializeComponent();
            // Tạo danh sách ngày từ 1 đến 31
            for (int i = 1; i <= 31; i++)
            {
                ComboBoxItem dayItem = new ComboBoxItem();
                dayItem.Content = i.ToString();
                DayComboBox.Items.Add(dayItem);
            }

            // Tạo danh sách tháng từ 1 đến 12
            for (int i = 1; i <= 12; i++)
            {
                ComboBoxItem monthItem = new ComboBoxItem();
                monthItem.Content = new DateTime(2000, i, 1).ToString("MMMM");
                MonthComboBox.Items.Add(monthItem);
            }

            // Tạo danh sách năm từ 1950 đến năm hiện tại
            int currentYear = DateTime.Now.Year;
            for (int i = 1950; i <= currentYear; i++)
            {
                ComboBoxItem yearItem = new ComboBoxItem();
                yearItem.Content = i.ToString();
                YearComboBox.Items.Add(yearItem);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/Account?retryWrites=true&w=majority"; // Thay thế bằng chuỗi kết nối của bạn
            string databaseName = "Account"; // Thay thế bằng tên của cơ sở dữ liệu của bạn
            string loggedInUserEmail = Connection.GetLoggedInUserEmail(); // Lấy email của người dùng đã đăng nhập

            // Lấy thông tin từ các trường nhập liệu
            string userBirthday = DayComboBox.SelectedItem + "/" + MonthComboBox.SelectedItem + "/" + YearComboBox.SelectedItem;
            string userIntroduce = IntroduceTextBox.Text;

            // Kiểm tra xem người dùng đã nhập đủ thông tin chưa
            if (!string.IsNullOrEmpty(loggedInUserEmail))
            {
                // Gọi hàm UpdateUserInformation từ lớp Connection để cập nhật thông tin vào tài khoản đã đăng nhập
                Connection conn = new Connection(connectionString, databaseName); // Tạo một đối tượng Connection
                conn.UpdateUserInformation(userBirthday, userIntroduce);
            }
        }

    }
}
