using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Prism.Commands;
using System.Windows.Controls;

namespace Nail_Salon_MVVM
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly string _connectionString;
        private readonly DatePicker _datePicker;

        public ICommand RecordClientCommand { get; private set; }
        public ICommand UpdateScheduleCommand { get; private set; }
        public ICommand DatePickerSelectedDateChangedCommand { get; }

        public MainViewModel(string connectionString, DatePicker ScheduleDatePicker)
        {
            _datePicker = ScheduleDatePicker;
            _connectionString = connectionString;

            RecordClientCommand = new DelegateCommand(OpenClientRecordingWindowAsync);
            UpdateScheduleCommand = new DelegateCommand(ScheduleUpdateCommand);
            DatePickerSelectedDateChangedCommand = new DelegateCommand(ScheduleDatePicker_SelectedDateChanged);

            ScheduleViewModel = new ScheduleItemViewModel(connectionString);
            EmployeesViewModel = new EmployeesViewModel(connectionString);
            SelectedDate = DateTime.Now;
        }

        private async void OpenClientRecordingWindowAsync()
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ClientRecordingWindow selectServiceWindow = new ClientRecordingWindow(_connectionString);
                    selectServiceWindow.ShowDialog();
                });
            });
        }


        private void ScheduleUpdateCommand()
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
                    viewModel.ScheduleViewModel.LoadScheduleItems(selectedDate, _connectionString);
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

