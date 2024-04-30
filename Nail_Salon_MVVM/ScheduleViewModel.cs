using Business_Logic;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Logic;

namespace Nail_Salon_MVVM
{
    public class ScheduleItemViewModel : INotifyPropertyChanged
    {

        public ScheduleItemViewModel()
        {
            ScheduleItems = new ObservableCollection<Schedule>();
            SelectedDate = DateTime.Now; // Установка текущей даты
            LoadScheduleItems(SelectedDate);
        }

        private ObservableCollection<Schedule> _scheduleItems;

        public ObservableCollection<Schedule> ScheduleItems
        {
            get { return _scheduleItems; }
            set
            {
                if (_scheduleItems != value)
                {
                    _scheduleItems = value;
                    OnPropertyChanged(nameof(ScheduleItems));
                }
            }
        }

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

        public void LoadDataForSelectedDate()
        {
            ScheduleItems.Clear();
            LoadScheduleItems(SelectedDate);
        }

        private void LoadScheduleItems(DateTime selectedDate)
        {
            ScheduleReader reader = new ScheduleReader("Data Source=MSI;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            List<Schedule> listScheduleitems = reader.getData(selectedDate);

            ScheduleItems.Clear();

            foreach (Schedule item in listScheduleitems)
            {
                ScheduleItems.Add(item);
            }

            OnPropertyChanged(nameof(ScheduleItems)); // Уведомление интерфейса об изменении коллекции
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
