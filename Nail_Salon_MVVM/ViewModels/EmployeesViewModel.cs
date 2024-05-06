using Business_Logic;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Nail_Salon_MVVM
{
    public class EmployeesViewModel : INotifyPropertyChanged
    {
        private readonly EmployeesReader _employeesReader;
        private readonly EmployeeReaderDbContext _dbContext;

        public EmployeesViewModel(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeReaderDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var dbContextOptions = optionsBuilder.Options;

            _dbContext = new EmployeeReaderDbContext(dbContextOptions); 
            _employeesReader = new EmployeesReader(dbContextOptions); 
            EmployeesItems = new ObservableCollection<Employee>();
            LoadScheduleItems();
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

        public void LoadDataForSelectedDate()
        {
            LoadScheduleItems();
        }

        private void LoadScheduleItems()
        {
            try
            {
                List<Employee> listEmployeesItems = _employeesReader.GetAllEmployeesAndCountScore();

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
