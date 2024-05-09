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
            GenerateTimeSlots();
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

        private async void Button_Record_Click(object sender, RoutedEventArgs e)
        {
            string fullName = ViewModel.fullName;
            DateTime? birthDate = ViewModel.birthDate;
            decimal phoneNumber = ViewModel.phoneNumber;
            DateOnly appointmentDate = ViewModel.SelectedDate;
            TimeSpan appointmentTime = ViewModel.SelectedTime;

            Customer client = new Customer { fullName = fullName, birthDate = birthDate, phoneNumber = phoneNumber };

            Service selectedService = ViewModel.SelectedService;
            Employee selectedEmployee = ViewModel.SelectedEmployee;

            if (client != null && selectedService != null && selectedEmployee != null)
            {
                DateTime appointmentDateTime = new DateTime(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day,
                                            appointmentTime.Hours, appointmentTime.Minutes, appointmentTime.Seconds);

                ClientRecord clientRecord = new ClientRecord(connectionString, client, selectedService, selectedEmployee, appointmentDateTime, selectedService.executionTime);

                using (ClientRecordDbContext context = new ClientRecordDbContext(connectionString))
                {
                    await clientRecord.GetClientRecording();
                    if (!clientRecord.RecordingIsSucsessfull)
                    {
                        string notificationText = $"{selectedEmployee.fullName} занят в это время";
                        ShowNotification(notificationText);
                    }
                    else
                    {
                        string notificationText = $"{fullName} записан(а) {appointmentDate} {ViewModel.SelectedTime} на {selectedService.name} к {selectedEmployee.fullName}";
                        ShowNotification(notificationText);
                    }
                }
            }
            else
            {
                string notificationText = "Не все данные введены. Пожалуйста, заполните все поля и выберите сервис и сотрудника";
                ShowNotification(notificationText);
            }
        }

        private void ShowNotification(string text)
        {
            NotificationWindow notificationWindow = new NotificationWindow(text);
            notificationWindow.ShowDialog();
        }

        private void GenerateTimeSlots()
        {
            const int startHour = 9;
            const int endHour = 20;
            const int intervalMinutes = 30;

            comboBoxTime.Items.Clear();

            for (int hour = startHour; hour <= endHour; hour++)
            {
                for (int minute = 0; minute < 60; minute += intervalMinutes)
                {
                    string time = $"{hour:D2}:{minute:D2}"; 

                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = time;

                    comboBoxTime.Items.Add(item);
                }
            }
        }
    }
}

