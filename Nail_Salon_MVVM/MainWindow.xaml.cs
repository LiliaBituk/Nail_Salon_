using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Nail_Salon_MVVM
{
    public partial class MainWindow : Window
    {
        private readonly string connectionString = "Data Source=MSI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;MultiSubnetFailover=False";

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel(connectionString);
            DataContext = viewModel;
        }

        private void ScheduleDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateScheduleData();
        }

        private async void Button_Client_Recording_Click(object sender, RoutedEventArgs e)
        {
            await OpenClientRecordingWindowAsync();
        }

        private async Task OpenClientRecordingWindowAsync()
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ClientRecordingWindow selectServiceWindow = new ClientRecordingWindow(connectionString);
                    selectServiceWindow.ShowDialog();
                });
            });
        }

        private void UpdateScheduleData()
        {
            if (DataContext is MainViewModel viewModel)
            {
                DateTime selectedDate = (DateTime)ScheduleDatePicker.SelectedDate;
                viewModel.SelectedDate = selectedDate;
                viewModel.ScheduleViewModel.LoadScheduleItems(selectedDate, connectionString);
            }
        }

        private void UpdateEmployeeTable()
        {
            if (DataContext is MainViewModel viewModel)
            {
                viewModel.EmployeesViewModel.GetEmployeesTable();
            }
        }

        private void Button_Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateScheduleData();
            UpdateEmployeeTable();
        }
    }
}

