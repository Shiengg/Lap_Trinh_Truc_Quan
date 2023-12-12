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
    }
}
