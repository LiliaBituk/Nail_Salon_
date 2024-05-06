using Business_Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace Nail_Salon_MVVM
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=MSI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;MultiSubnetFailover=False";

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel();
            DataContext = viewModel;
        }

        private void ScheduleDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is MainViewModel viewModel)
            {
                DateTime selectedDate = (DateTime)ScheduleDatePicker.SelectedDate;
                viewModel.SelectedDate = selectedDate; // Установка выбранной даты во ViewModel
                viewModel.ScheduleViewModel.LoadDataForSelectedDate(selectedDate);
            }
        }

        private void Button_Client_Recording_Click(object sender, RoutedEventArgs e)
        {
            ClientRecordingWindow selectServiceWindow = new ClientRecordingWindow(connectionString);
            selectServiceWindow.ShowDialog();
        }
    }
}

