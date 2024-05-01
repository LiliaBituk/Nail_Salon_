//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Business_Logic
//{
//    public class Employees
//    {
//        public string EmployeeName { get; set; }
//        public string ServiceType { get; set; }
//        public decimal PhoneNumber { get; set; }
//        public int Score { get; set; }
//    }
//}

using System.ComponentModel;

namespace Business_Logic
{
    public class Employees : INotifyPropertyChanged
    {
        private string _employeeName;
        public string EmployeeName
        {
            get { return _employeeName; }
            set
            {
                if (_employeeName != value)
                {
                    _employeeName = value;
                    OnPropertyChanged(nameof(EmployeeName));
                }
            }
        }

        private string _serviceType;
        public string ServiceType
        {
            get { return _serviceType; }
            set
            {
                if (_serviceType != value)
                {
                    _serviceType = value;
                    OnPropertyChanged(nameof(ServiceType));
                }
            }
        }

        private decimal _phoneNumber;
        public decimal PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(PhoneNumber));
                }
            }
        }

        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged(nameof(Score));
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
