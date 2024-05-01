////namespace Business_Logic
////{
////    public class Schedule
////    {
////        public string CustomerName { get; set; }
////        public string ServiceName { get; set; }
////        public DateTime StartDataTime { get; set; }
////        public string EmployeeName { get; set; }
////        public decimal Price { get; set; }
////    }
////}

using System.ComponentModel;

namespace Business_Logic
{
    public class Schedule : INotifyPropertyChanged
    {
        private string _customerName;
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged(nameof(CustomerName));
                }
            }
        }

        private string _serviceName;
        public string ServiceName
        {
            get { return _serviceName; }
            set
            {
                if (_serviceName != value)
                {
                    _serviceName = value;
                    OnPropertyChanged(nameof(ServiceName));
                }
            }
        }

        private DateTime _startDateTime;
        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set
            {
                if (_startDateTime != value)
                {
                    _startDateTime = value;
                    OnPropertyChanged(nameof(StartDateTime));
                }
            }
        }

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

        private decimal _price;
        public decimal Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(Price));
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


//namespace Business_Logic
//{
//    public class Schedule
//    {
//        public string CustomerName { get; set; }
//        public string ServiceName { get; set; }
//        public DateTime StartDateTime { get; set; }
//        public string EmployeeName { get; set; }
//        public decimal Price { get; set; }
//    }
//}