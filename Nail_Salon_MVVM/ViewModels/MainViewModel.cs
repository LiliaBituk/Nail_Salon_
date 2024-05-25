using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Prism.Commands;
using System.Windows.Controls;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Nail_Salon_MVVM
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly string _connectionString;
        private readonly DatePicker _datePicker;

        public ICommand _recordClientCommand { get; private set; }
        public ICommand _updateScheduleCommand { get; private set; }
        public ICommand _datePickerSelectedDateChangedCommand { get; }

        public IRepositoryFactory _repositoryFactory;

        public MainViewModel(string connectionString, DatePicker ScheduleDatePicker)
        {
            _datePicker = ScheduleDatePicker;
            _connectionString = connectionString;

            _recordClientCommand = new DelegateCommand(OpenClientRecordingWindowAsync);
            _updateScheduleCommand = new DelegateCommand(UpdateScheduleCommand);
            _datePickerSelectedDateChangedCommand = new DelegateCommand(ScheduleDatePicker_SelectedDateChanged);

            var optionsBuilder = new DbContextOptionsBuilder<ReadingDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            _repositoryFactory = new RepositoryFactory(optionsBuilder.Options, connectionString);

            ScheduleViewModel = new ScheduleItemViewModel(_repositoryFactory);
            EmployeesViewModel = new EmployeesViewModel(_repositoryFactory);
            SelectedDate = DateTime.Now;
        }

        private async void OpenClientRecordingWindowAsync()
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ClientRecordingWindow selectServiceWindow = new ClientRecordingWindow(_connectionString, _repositoryFactory);
                    selectServiceWindow.ShowDialog();
                });
            });
        }

        private void UpdateScheduleCommand()
        {
            UpdateScheduleData();
            UpdateEmployeeTable();
        }

        private void ScheduleDatePicker_SelectedDateChanged()
        {
            UpdateScheduleData();
        }

        private void UpdateScheduleData()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (this is MainViewModel viewModel)
                {
                    DateTime selectedDate = (DateTime)_datePicker.SelectedDate;
                    viewModel.SelectedDate = selectedDate;
                    viewModel.ScheduleViewModel.LoadScheduleItems(selectedDate);
                }
            });
        }

        private void UpdateEmployeeTable()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (this is MainViewModel viewModel)
                {
                    viewModel.EmployeesViewModel.GetEmployeesTable();
                }
            });
        }


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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

