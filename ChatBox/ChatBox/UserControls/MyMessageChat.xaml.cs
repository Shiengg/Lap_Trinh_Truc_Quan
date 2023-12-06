using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ChatBox.UserControls
{
    /// <summary>
    /// Interaction logic for MyMessageChat.xaml
    /// </summary>
    public partial class MyMessageChat : UserControl
    {
        public MyMessageChat()
        {
            InitializeComponent();
        }
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(MyMessageChat));


        private string _message;
        public string MyMessage
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(MyMessage));
            }
        }

         public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    }
}
