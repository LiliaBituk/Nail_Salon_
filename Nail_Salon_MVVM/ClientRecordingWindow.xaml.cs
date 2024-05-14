using System.Windows;
using System.Windows.Controls;
using Business_Logic;

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

            ServiceComboBox.SelectionChanged += (sender, e) => ViewModel.SelectedService = (sender as ComboBox).SelectedItem as Service;
            EmployeeComboBox.SelectionChanged += (sender, e) => ViewModel.SelectedEmployee = (sender as ComboBox).SelectedItem as Employee;
        }

    }
}

