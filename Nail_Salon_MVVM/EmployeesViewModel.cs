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
            EmployeesItems = new ObservableCollection<Employees>();
            LoadScheduleItems();
        }

        private ObservableCollection<Employees> _employeesItems;

        public ObservableCollection<Employees> EmployeesItems
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
            List<Employees> listEmployeesItems = reader.getData();

            EmployeesItems.Clear();

            foreach (Employees item in listEmployeesItems)
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
