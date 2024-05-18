using System.Windows;

namespace Nail_Salon_MVVM
{
    public partial class ClientRecordingWindow : Window
    {
        public ClientRecordingViewModel ViewModel { get; set; }

        public ClientRecordingWindow(string connectionString)
        {
            InitializeComponent();
            ViewModel = new ClientRecordingViewModel(connectionString, ServiceComboBox, EmployeeComboBox, TimeComboBox);
            DataContext = ViewModel;
        }
    }
}

