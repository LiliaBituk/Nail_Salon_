using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Business_Logic
{
    public class Schedule : INotifyPropertyChanged
    {
        [Key]
        [Column("id")]
        public int id { get; set; }

        private string _customerName;
        public string customerName
        {
            get { return _customerName; }
            set
            {
                if (_customerName != value)
                {
                    _customerName = value;
                    OnPropertyChanged(nameof(customerName));
                }
            }
        }

        private string _serviceName;
        public string serviceName
        {
            get { return _serviceName; }
            set
            {
                if (_serviceName != value)
                {
                    _serviceName = value;
                    OnPropertyChanged(nameof(serviceName));
                }
            }
        }

        private DateTime _startDateTime;
        public DateTime startDateTime
        {
            get { return _startDateTime; }
            set
            {
                if (_startDateTime != value)
                {
                    _startDateTime = value;
                    OnPropertyChanged(nameof(startDateTime));
                }
            }
        }

        private string _employeeName;
        public string employeeName
        {
            get { return _employeeName; }
            set
            {
                if (_employeeName != value)
                {
                    _employeeName = value;
                    OnPropertyChanged(nameof(employeeName));
                }
            }
        }

        private decimal _price;
        public decimal price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    OnPropertyChanged(nameof(price));
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
