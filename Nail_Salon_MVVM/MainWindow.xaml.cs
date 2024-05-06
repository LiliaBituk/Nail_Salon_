using System;
using System.Windows;
using System.Windows.Controls;

namespace Nail_Salon_MVVM
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=MSI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;MultiSubnetFailover=False";

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel(connectionString);
            DataContext = viewModel;
        }

        private void ScheduleDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                DateTime selectedDate = (DateTime)ScheduleDatePicker.SelectedDate;
                viewModel.SelectedDate = selectedDate;
                viewModel.ScheduleViewModel.LoadScheduleItems(selectedDate, connectionString);
            }
        }

        private void Button_Client_Recording_Click(object sender, RoutedEventArgs e)
        {
            ClientRecordingWindow selectServiceWindow = new ClientRecordingWindow(connectionString);
            selectServiceWindow.ShowDialog();
        }
    }
}

