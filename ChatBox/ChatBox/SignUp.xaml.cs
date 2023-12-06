using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.Mail;

namespace ChatBox
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }
        public bool ValidateEmail(string email) //check email
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public bool CheckPasswordLength(string password)
        {
            return password.Length >= 8;
        }
        Modify modify = new Modify();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Email = txtEmail.Text;
            string Pass = txtPassword.Password;
            string cPass = txtConfirmPassword.Password;
            if(!ValidateEmail(Email)) { MessageBox.Show("Please enter correct email format"); return; }
            if(!CheckPasswordLength(Pass)) { MessageBox.Show("Please enter at least 8 characters"); return; }
            if(cPass != Pass) { MessageBox.Show("Please confirm your password again"); return; }
            string queryCheckExisting = "SELECT COUNT(*) FROM ACCOUNT WHERE Email = @Email";
            int existingAccounts = modify.CountAccounts(queryCheckExisting, Email);

            if (existingAccounts > 0)
            {
                MessageBox.Show("Email is already registered");
                return;
            }

            try
            {
                string queryInsert = "INSERT INTO ACCOUNT (Email, Pass) VALUES (@Email, @Pass)";
                modify.InsertAccount(queryInsert, Email, Pass);
                MessageBox.Show("Registered successfully");
                Login f= new Login();
                f.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registering: {ex.Message}");
            }
        }

        private void textEmail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtEmail.Focus();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Length > 0)
            {
                textEmail.Visibility = Visibility.Collapsed;
            }
            else
            {
                textEmail.Visibility = Visibility.Visible;
            }
        }

        private void textPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPassword.Focus();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                textPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPassword.Visibility = Visibility.Visible;
            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPassword.Password))
        //    {
        //        MessageBox.Show("Sucessfully Sign In");
        //    }
        //}

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void textConfirmPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtConfirmPassword.Focus();
        }

        private void txtConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtConfirmPassword.Password) && txtConfirmPassword.Password.Length > 0)
            {
                textConfirmPassword.Visibility = Visibility.Collapsed;
            }
            else
            {
                textConfirmPassword.Visibility = Visibility.Visible;
            }
        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string url = "https://www.facebook.com/login.php/"; // Thay thế URL này bằng URL thực của trang Facebook

        //        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        //        {
        //            FileName = url,
        //            UseShellExecute = true
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý ngoại lệ nếu có lỗi khi mở trình duyệt
        //        Console.WriteLine("Lỗi: " + ex.Message);
        //    }
        //}

        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        string emailAddress = "https://mail.google.com/"; // Thay thế địa chỉ email này bằng địa chỉ email thực

        //        System.Diagnostics.Process.Start($"mailto:{emailAddress}");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý ngoại lệ nếu có lỗi khi mở ứng dụng email
        //        Console.WriteLine("Lỗi: " + ex.Message);
        //    }
        //}
    }
}
