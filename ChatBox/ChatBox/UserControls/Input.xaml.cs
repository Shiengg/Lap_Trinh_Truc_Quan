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

namespace ChatBox.UserControls
{
    /// <summary>
    /// Interaction logic for Input.xaml
    /// </summary>
    public partial class Input : UserControl
    {
        public static readonly DependencyProperty InputTextProperty =
            DependencyProperty.Register("InputText", typeof(string), typeof(Input), new PropertyMetadata(""));

        public string Message
        {
            get { return (string)GetValue(InputTextProperty); }
            set { SetValue(InputTextProperty, value); }
        }
        public Input()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
