using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Business_Logic;
using DataAccess;

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

                LoadEmployeesByServiceType(value?.type);
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
        public TimeSpan executionTime
        {
            get { return _executionTime; }
            set
            {
                _executionTime = value;
                OnPropertyChanged(nameof(executionTime));
            }
        }

        private string _fullName;
        public string fullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(fullName));
            }
        }

        private DateTime? _birthDate;
        public DateTime? birthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(birthDate));
            }
        }

        private decimal _phoneNumber;
        public decimal phoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(phoneNumber));
            }
        }

        private readonly string _connectionString;

        public ClientRecordingViewModel(string connectionString)
        {
            _connectionString = connectionString;

            LoadServices();
        }

        private async Task LoadServices()
        {
            AvailableServices = new ObservableCollection<Service>();
            AvailableEmployees = new ObservableCollection<Employee>();

            var optionsBuilder = new DbContextOptionsBuilder<ServiceReaderDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            var reader = new ServiceReader(optionsBuilder.Options);
            List<Service> services = await reader.GetAllServices();

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
                var optionsBuilder = new DbContextOptionsBuilder<EmployeeReaderDbContext>();
                optionsBuilder.UseSqlServer(_connectionString);

                var employeesReader = new EmployeesReader(optionsBuilder.Options);
                List<Employee> employees = await employeesReader.GetEmployeesByServiceType(serviceType);

                AvailableEmployees.Clear();

                foreach (Employee employee in employees)
                {
                    AvailableEmployees.Add(employee);
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
