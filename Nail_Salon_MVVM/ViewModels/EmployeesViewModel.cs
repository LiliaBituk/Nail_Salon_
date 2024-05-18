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
        private readonly EmployeesReader _employeesReader;
        private readonly ReadingDbContext _dbContext;

        public EmployeesViewModel(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ReadingDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var dbContextOptions = optionsBuilder.Options;

            _dbContext = new ReadingDbContext(dbContextOptions); 
            _employeesReader = new EmployeesReader(dbContextOptions); 
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
                List<Employee> listEmployeesItems = await _employeesReader.GetAllEmployeesAndCountScore();

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
