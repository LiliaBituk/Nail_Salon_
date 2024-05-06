using System;
using System.ComponentModel;

namespace Nail_Salon_MVVM
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ScheduleItemViewModel ScheduleViewModel { get; }
        public EmployeesViewModel EmployeesViewModel { get; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                if (_selectedDate != value)
                {
                    _selectedDate = value;
                    OnPropertyChanged(nameof(SelectedDate));
                }
            }
        }

        public MainViewModel(string connectionString)
        {
            ScheduleViewModel = new ScheduleItemViewModel(connectionString);
            EmployeesViewModel = new EmployeesViewModel(connectionString);
            SelectedDate = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

