using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Business_Logic;
using DataAccess;

namespace Nail_Salon_MVVM
{
    public class EmployeesViewModel : INotifyPropertyChanged
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IEmployeeRepository reader;

        public EmployeesViewModel(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
            reader = _repositoryFactory.CreateEmployeeRepository();

            EmployeesItems = new ObservableCollection<Employee>();
            GetEmployeesTable();
        }

        private ObservableCollection<Employee> _employeesItems;

        public ObservableCollection<Employee> EmployeesItems
        {
            get { return _employeesItems; }
            set
            {
                if (_employeesItems != value)
                {
                    _employeesItems = value;
                    OnPropertyChanged(nameof(EmployeesItems));
                }
            }
        }

        public void GetEmployeesTable()
        {
            LoadScheduleItems();
        }

        private async void LoadScheduleItems()
        {
            try
            {
                List<Employee> listEmployeesItems = await reader.GetAllEmployeesAndCountScore();

                EmployeesItems.Clear();
                foreach (Employee item in listEmployeesItems)
                {
                    EmployeesItems.Add(item);
                }

                OnPropertyChanged(nameof(EmployeesItems));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
