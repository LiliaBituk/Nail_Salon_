using System;
using System.Windows;
using System.Windows.Controls;
using Business_Logic;
using DataAccess;

namespace Nail_Salon_MVVM
{
    public partial class ClientRecordingWindow : Window
    {
        public ClientRecordingViewModel ViewModel { get; set; }
        private string connectionString;

        public ClientRecordingWindow(string connectionString)
        {
            InitializeComponent();
            ViewModel = new ClientRecordingViewModel();
            DataContext = ViewModel;
            this.connectionString = connectionString;
        }

        private void Service_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedService = (sender as ComboBox).SelectedItem as Service;

            ViewModel.SelectedService = selectedService;
            //GetEmployeesByServiceType(string serviceType);
        }

        private void Employee_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmployee = (sender as ComboBox).SelectedItem as Employee;

            ViewModel.SelectedEmployee = selectedEmployee;
            //GetEmployeesByServiceType(string serviceType);
        }


        private void Button_Record_Click(object sender, RoutedEventArgs e)
        {
            string fullName = ViewModel.fullName;
            DateTime? birthDate = ViewModel.birthDate;
            decimal phoneNumber = ViewModel.phoneNumber;

            Customer client = new Customer { fullName=fullName, birthDate=birthDate, phoneNumber=phoneNumber };

            Service selectedService = ViewModel.SelectedService;
            Employee selectedEmployee = ViewModel.SelectedEmployee;

            if (client != null && selectedService != null && selectedEmployee != null)
            {
                ClientRecord clientRecord = new ClientRecord(connectionString, client, selectedService, selectedEmployee);

                clientRecord.ClientRecording();
            }
            else
            {
                MessageBox.Show("Не все данные введены. Пожалуйста, заполните все поля и выберите сервис и сотрудника.");
            }
        }
    }
}

