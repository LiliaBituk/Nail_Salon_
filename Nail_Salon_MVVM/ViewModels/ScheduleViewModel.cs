using Business_Logic;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Nail_Salon_MVVM
{
    public class ScheduleItemViewModel : INotifyPropertyChanged
    {
        public ScheduleItemViewModel(string connectionString)
        {
            ScheduleItems = new ObservableCollection<Schedule>();
            SelectedDate = DateTime.Now;
            LoadScheduleItems(SelectedDate, connectionString);
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

        public void LoadScheduleItems(DateTime selectedDate, string connectionString)
        {
            try
            {
                var optionsBuilder = new DbContextOptionsBuilder<ScheduleReaderDbContext>();
                optionsBuilder.UseSqlServer(connectionString);
                var dbContextOptions = optionsBuilder.Options;

                ScheduleReader reader = new ScheduleReader(dbContextOptions);
                List<Schedule> listScheduleitems = reader.GetSchedule(selectedDate);

                ScheduleItems.Clear();

                foreach (Schedule item in listScheduleitems)
                {
                    ScheduleItems.Add(item);
                }

                OnPropertyChanged(nameof(ScheduleItems));
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
