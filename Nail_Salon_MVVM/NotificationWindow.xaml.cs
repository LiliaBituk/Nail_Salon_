using System.Windows;

namespace Nail_Salon_MVVM
{
    public partial class NotificationWindow : Window
    {
        public string NotificationText { get; set; }

        public NotificationWindow(string notificationText)
        {
            InitializeComponent();
            NotificationText = notificationText;
            DataContext = this;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
