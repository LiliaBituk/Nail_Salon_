using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Business_Logic;
using DataAccess;
using Microsoft.EntityFrameworkCore;

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

        private readonly string connectionString;

        public ClientRecordingViewModel(string connectionString)
        {
            this.connectionString = connectionString;

            var EmployeeReaderOptionsBuilder = new DbContextOptionsBuilder<EmployeeReaderDbContext>();
            EmployeeReaderOptionsBuilder.UseSqlServer(this.connectionString);

            var ServiceReaderOptionsBuilder = new DbContextOptionsBuilder<ServiceReaderDbContext>();
            ServiceReaderOptionsBuilder.UseSqlServer(this.connectionString);

            ServiceReader reader = new ServiceReader(ServiceReaderOptionsBuilder.Options);
            AvailableServices = new ObservableCollection<Service>(reader.GetAllServices());
            AvailableEmployees = new ObservableCollection<Employee>();
        }

        private void LoadEmployeesByServiceType(string serviceType)
        {
            AvailableEmployees.Clear();

            if (serviceType != null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<EmployeeReaderDbContext>();
                optionsBuilder.UseSqlServer(connectionString);

                var employeesReader = new EmployeesReader(optionsBuilder.Options);
                List<Employee> employees = employeesReader.GetEmployeesByServiceType(serviceType);

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
