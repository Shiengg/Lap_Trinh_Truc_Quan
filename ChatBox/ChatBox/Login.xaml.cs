using MongoDB.Driver.Core.Configuration;
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
using System.Windows.Shapes;
using MongoDB.Driver;
using Amazon.Runtime.Internal;

namespace ChatBox
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        Modify modify = new Modify();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string pass = txtPassword.Password;
            if (email.Trim() == "")
            {
                loginStatus.Text = "Please enter your email!";
            }
            else if (pass.Trim() == "")
            {
                
                loginStatus.Text = "Please enter your password!";
            }
            else
            {
                Connection modify = new Connection(@"mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/QLChatbox?retryWrites=true&w=majority", "Account");

                List<Account> accounts = modify.GetAccounts(email);

                if (accounts.Count != 0)
                {
                    loginStatus.Text = "Login Successfully!";
                    loginStatus.Foreground = Brushes.Green;
                    MainWindow f = new MainWindow();
                    f.Show();
                    
                }
                else
                {
                    loginStatus.Text = "Login failed!";
                }
            }
            //if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtPassword.Password))
            //{
            //    MainWindow f = new MainWindow();
            //    f.Show();
            //}
            //else
            //{
            //    MessageBox.Show("Login failed");
            //}
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SignUp f = new SignUp();
            f.Show();
        }
    }
}
