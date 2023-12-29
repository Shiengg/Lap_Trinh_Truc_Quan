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
                Connection connection = new Connection(@"mongodb+srv://22521708:HgecVbTzd1Iqz6fx@cluster0.um2tiwy.mongodb.net/QLChatbox?retryWrites=true&w=majority", "Account");

                List<Account> accounts = connection.GetAccounts(email);

                bool loginSuccess = false;

                foreach (Account userAccount in accounts)
                {
                    if (userAccount.Email == email && userAccount.Password == pass)
                    {
                        loginSuccess = true;
                        break; 
                    }
                }

                if (loginSuccess)
                {
                    loginStatus.Text = "Login Successfully!";
                    Connection.SetLoggedInUserEmail(email);
                    Connection.SetLoggedInUserPass(pass);
                    loginStatus.Foreground = Brushes.Green;
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Window.GetWindow(this).Close();
                }
                else
                {
                    loginStatus.Text = "Incorrect email or password!";
                }
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
            Window.GetWindow(this).Close();
        }

        
    }
}
