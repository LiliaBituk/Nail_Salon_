﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Prism.Commands;
using System.Windows.Controls;
using DataAccess;

namespace Nail_Salon_MVVM
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly DatePicker _datePicker;

        public ICommand _recordClientCommand { get; private set; }
        public ICommand _updateScheduleCommand { get; private set; }
        public ICommand _deleteAppointmentCommand { get; private set; }
        public ICommand _datePickerSelectedDateChangedCommand { get; }

        public IRepositoryFactory _repositoryFactory;

        public ScheduleItemViewModel ScheduleViewModel { get; }
        public EmployeesViewModel EmployeesViewModel { get; }

        public MainViewModel(string connectionString, DatePicker ScheduleDatePicker)
        {
            _datePicker = ScheduleDatePicker;

            _recordClientCommand = new DelegateCommand(OpenClientRecordingWindowAsync);
            _updateScheduleCommand = new DelegateCommand(UpdateScheduleCommand);
            _deleteAppointmentCommand = new DelegateCommand(DeleteAppointmentCommand);
            _datePickerSelectedDateChangedCommand = new DelegateCommand(ScheduleDatePicker_SelectedDateChanged);

            _repositoryFactory = new RepositoryFactory(connectionString);

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
                    ClientRecordingWindow selectServiceWindow = new ClientRecordingWindow(_repositoryFactory);
                    selectServiceWindow.ShowDialog();
                });
            });
        }

        private async void DeleteAppointmentCommand()
        {
            if (ScheduleViewModel.SelectedScheduleItem != null)
            {
                int appointmentId = ScheduleViewModel.SelectedScheduleItem.Id; 
                bool isDeleted = await _repositoryFactory.CreateScheduleRepository().DeleteAppointment(appointmentId);

                if (isDeleted)
                {
                    UpdateScheduleData();
                    ShowNotification("Запись удалена");
                }
                else
                {
                    ShowNotification("Не удалось удалить запись");
                }
            }
            else
            {
                ShowNotification("Пожалуйста, выберите запись для удаления");
            }
        }

        private void ShowNotification(string text)
        {
            NotificationWindow notificationWindow = new NotificationWindow(text);
            notificationWindow.ShowDialog();
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

