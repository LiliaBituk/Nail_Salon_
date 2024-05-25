using DataAccess;
using System.Windows;

namespace Nail_Salon_MVVM
{
    public partial class ClientRecordingWindow : Window
    {
        public ClientRecordingViewModel ViewModel { get; set; }

        public ClientRecordingWindow(string connectionString, IRepositoryFactory repositoryFactory)
        {
            InitializeComponent();
            ViewModel = new ClientRecordingViewModel(connectionString, repositoryFactory, ServiceComboBox, EmployeeComboBox, TimeComboBox);
            DataContext = ViewModel;
        }
    }
}

