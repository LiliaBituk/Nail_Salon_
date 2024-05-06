using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        //private Employee _selectedEmployeeId;
        //public Employee SelectedEmployeeId
        //{
        //    get { return _selectedEmployeeId; }
        //    set
        //    {
        //        _selectedEmployeeId = value;
        //        OnPropertyChanged(nameof(SelectedEmployeeId));
        //    }
        //}

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

        public ClientRecordingViewModel()
        {
            ServiceReader reader = new ServiceReader("Data Source=MSI;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            AvailableServices = new ObservableCollection<Service>(reader.GetAllServices());
            AvailableEmployees = new ObservableCollection<Employee>();
        }

        private void LoadEmployeesByServiceType(string serviceType)
        {
            AvailableEmployees.Clear();

            if (serviceType != null)
            {
                EmployeesReader employeesReader = new EmployeesReader("Data Source=MSI;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
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
