using System.Windows;
using System.Windows.Input;

namespace Nail_Salon_MVVM
{
    public partial class NotificationWindow : Window
    {
        public NotificationViewModel ViewModel { get; set; }
        public ICommand OkButtonCommand
        {
            get { return ViewModel.OkButtonCommand; }
        }

        public string NotificationText { get; set; }

        public NotificationWindow(string notificationText)
        {
            InitializeComponent();
            NotificationText = notificationText;
            DataContext = this;

            ViewModel = new NotificationViewModel(this);
        }
    }
}
