﻿using Business_Logic;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;


namespace Nail_Salon_MVVM
{
    public class ClientRecordingViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Service> AvailableServices { get; set; }
        public ObservableCollection<Employee> AvailableEmployees { get; set; }

        private Service _selectedService;
        public Service SelectedService
        {
            get { return _selectedService; }
            set
            {
                _selectedService = value;
                OnPropertyChanged(nameof(SelectedService));

                if (value != null)
                {
                    LoadEmployeesByServiceType(value.ServiceType);
                }
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged(nameof(SelectedEmployee));
            }
        }

        private DateOnly _selectedDate;
        public DateOnly SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private TimeSpan _selectedTime;
        public TimeSpan SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                _selectedTime = value;
                OnPropertyChanged(nameof(SelectedTime));
            }
        }

        private TimeSpan _executionTime;
        public TimeSpan ExecutionTime
        {
            get { return _executionTime; }
            set
            {
                _executionTime = value;
                OnPropertyChanged(nameof(ExecutionTime));
            }
        }

        private string _fullName;
        public string CustomerFullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(CustomerFullName));
            }
        }

        private DateTime _birthDate;
        public DateTime CustomerBirthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(CustomerBirthDate));
            }
        }

        private decimal _phoneNumber;
        public decimal CustomerPhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(CustomerPhoneNumber));
            }
        }

        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IServiceRepository _serviceRepository;

        private readonly string _connectionString;

        public ICommand ServiceComboBoxSelectionChangedCommand { get; }
        public ICommand EmployeeComboBoxSelectionChangedCommand { get; }
        public ICommand ButtonCustomerWritingCommand { get; }

        public ComboBox _servicesComboBox;
        public ComboBox _employeesComboBox;
        public ComboBox _timeComboBox;

        public ClientRecordingViewModel(string connectionString, IRepositoryFactory repositoryFactory, ComboBox ServiceComboBox, ComboBox EmployeeComboBox, ComboBox TimeComboBox)
        {
            _connectionString = connectionString;

            _repositoryFactory = repositoryFactory;
            _serviceRepository = _repositoryFactory.CreateServiceRepository();

            _servicesComboBox = ServiceComboBox;
            _employeesComboBox = EmployeeComboBox;
            _timeComboBox = TimeComboBox;

            ServiceComboBoxSelectionChangedCommand = new DelegateCommand(Service_ComboBox_SelectionChanged);
            EmployeeComboBoxSelectionChangedCommand = new DelegateCommand(Employee_ComboBox_SelectionChanged);
            ButtonCustomerWritingCommand = new DelegateCommand(Button_Record_Click);

            GenerateTimeSlots();
            LoadServices();
        }

        private async Task LoadServices()
        {
            AvailableServices = new ObservableCollection<Service>();
            AvailableEmployees = new ObservableCollection<Employee>();

            List<Service> services = await _serviceRepository.GetAllServices();

            AvailableServices.Clear();

            foreach (var service in services)
            {
                AvailableServices.Add(service);
            }
        }

        private async void LoadEmployeesByServiceType(string serviceType)
        {
            if (serviceType != null)
            {
                IEmployeeRepository employeesReader = _repositoryFactory.CreateEmployeeRepository();
                List<Employee> employees = await employeesReader.GetEmployeesByServiceType(serviceType);

                AvailableEmployees.Clear();

                foreach (Employee employee in employees)
                {
                    AvailableEmployees.Add(employee);
                }
            }
        }

        private void Service_ComboBox_SelectionChanged()
        {
            var selectedService = _servicesComboBox.SelectedItem as Service;
            SelectedService = selectedService;
        }


        private void Employee_ComboBox_SelectionChanged()
        {
            var selectedEmployee = _employeesComboBox.SelectedItem as Employee;
            SelectedEmployee = selectedEmployee;
        }

        private void Button_Record_Click()
        {
            string fullName = CustomerFullName;
            DateTime birthDate = CustomerBirthDate;
            decimal phoneNumber = CustomerPhoneNumber;
            DateOnly appointmentDate = SelectedDate;
            TimeSpan appointmentTime = SelectedTime;

            Customer client = new Customer { CustomerFullName = fullName, CustomerBirthDate = birthDate, CustomerPhoneNumber = phoneNumber };

            Service selectedService = SelectedService;
            Employee selectedEmployee = SelectedEmployee;

            if (client != null && selectedService != null && selectedEmployee != null)
            {
                DateTime appointmentDateTime = new DateTime(appointmentDate.Year, appointmentDate.Month, appointmentDate.Day,
                                            appointmentTime.Hours, appointmentTime.Minutes, appointmentTime.Seconds);

                ICustomerRepository clientRecord = _repositoryFactory.CreateCustomerRepository(client, selectedService, selectedEmployee, appointmentDateTime, selectedService.ServiceExecutionTime);
                using (WritingDbContext context = new WritingDbContext(_connectionString))
                {
                    clientRecord.RecordCustomerAsync();
                    if (!clientRecord.IsRecordingSuccessful)
                    {
                        string notificationText = $"{selectedEmployee.EmployeeFullName} занят в это время";
                        ShowNotification(notificationText);
                    }
                    else
                    {
                        string notificationText = $"{fullName} записан(а) {appointmentDate} {SelectedTime} на {selectedService.ServiceName} к {selectedEmployee.EmployeeFullName}";
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
            // сделать вытягивание времени из бд

            const int startHour = 9;
            const int endHour = 20;
            const int intervalMinutes = 30;

            _timeComboBox.Items.Clear();

            for (int hour = startHour; hour <= endHour; hour++)
            {
                for (int minute = 0; minute < 60; minute += intervalMinutes)
                {
                    string time = $"{hour:D2}:{minute:D2}";

                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = time;

                    _timeComboBox.Items.Add(item);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
