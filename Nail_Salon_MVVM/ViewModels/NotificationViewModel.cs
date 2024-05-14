using Prism.Commands;
using System.Windows;
using System.Windows.Input;

namespace Nail_Salon_MVVM
{
    public class NotificationViewModel
    {
        public ICommand OkButtonCommand { get; }
        
        public Window _window;

        public NotificationViewModel(Window window)
        {
            _window = window;

            OkButtonCommand = new DelegateCommand(OK_Click);
        }
        private void OK_Click()
        {
            _window.Close();
        }
    }
}
