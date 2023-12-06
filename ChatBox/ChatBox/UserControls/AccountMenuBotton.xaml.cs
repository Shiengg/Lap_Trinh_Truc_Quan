using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChatBox.UserControls
{
    /// <summary>
    /// Interaction logic for AccountMenuBotton.xaml
    /// </summary>
    public partial class AccountMenuBotton : UserControl
    {
        public AccountMenuBotton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title",typeof(string),typeof(AccountMenuBotton));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(string), typeof(AccountMenuBotton));

        public Color GradientColor1
        {
            get { return (Color)GetValue(GradientColor1Property); }
            set { SetValue(GradientColor1Property, value); }
        }
        public static readonly DependencyProperty GradientColor1Property = DependencyProperty.Register("GradientColor1", typeof(Color), typeof(AccountMenuBotton));

        public Color GradientColor2
        {
            get { return (Color)GetValue(GradientColor2Property); }
            set { SetValue(GradientColor2Property, value); }
        }
        public static readonly DependencyProperty GradientColor2Property = DependencyProperty.Register("GradientColor2", typeof(Color), typeof(AccountMenuBotton));
    }
}
