using Business_Logic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nail_Salon_MVVM
{

    public class EmployeesViewModel : INotifyPropertyChanged
    {

        public EmployeesViewModel()
        {
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
            EmployeesItems.Clear();
            LoadScheduleItems();
        }

        private void LoadScheduleItems()
        {
            EmployeesReader reader = new EmployeesReader("Data Source=MSI;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            List<Employee> listEmployeesItems = reader.GetAllEmployeesAndCountScore();

            EmployeesItems.Clear();

            foreach (Employee item in listEmployeesItems)
            {
                EmployeesItems.Add(item);
            }

            OnPropertyChanged(nameof(EmployeesItems)); // Уведомление интерфейса об изменении коллекции
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
