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
            ViewModel = new ClientRecordingViewModel(connectionString);
            DataContext = ViewModel;
            this.connectionString = connectionString;
        }

        private void Service_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedService = (sender as ComboBox).SelectedItem as Service;

            ViewModel.SelectedService = selectedService;
        }

        private void Employee_ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedEmployee = (sender as ComboBox).SelectedItem as Employee;

            ViewModel.SelectedEmployee = selectedEmployee;
        }


        private void Button_Record_Click(object sender, RoutedEventArgs e)
        {
            string fullName = ViewModel.fullName;
            DateTime? birthDate = ViewModel.birthDate;
            decimal phoneNumber = ViewModel.phoneNumber;
            DateOnly appointmentDate = ViewModel.SelectedDate;
            TimeSpan appointmentTime = ViewModel.SelectedTime;

            Customer client = new Customer { fullName=fullName, birthDate=birthDate, phoneNumber=phoneNumber };

            Service selectedService = ViewModel.SelectedService;
            Employee selectedEmployee = ViewModel.SelectedEmployee;

            if (client != null && selectedService != null && selectedEmployee != null)
            {
                DateTime appointmentDateTime = new DateTime(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day,
                                            appointmentTime.Hours, appointmentTime.Minutes, appointmentTime.Seconds);

                ClientRecord clientRecord = new ClientRecord(connectionString, client, selectedService, selectedEmployee, appointmentDateTime);
                clientRecord.ClientRecording();

                if (!clientRecord.RecordingIsSucsessfull)
                {
                    MessageBox.Show("Мастер занят в это время", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"{fullName} записан(а) {appointmentDate} {ViewModel.SelectedTime} на {selectedService.name} к {selectedEmployee.fullName}", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Не все данные введены. Пожалуйста, заполните все поля и выберите сервис и сотрудника.");
            }
        }
    }
}

