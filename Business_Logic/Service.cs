using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business_Logic
{
    public class Service : INotifyPropertyChanged
    {
        [Key]
        public int id { get; set; }

        private string _serviceName;
        public string name
        {
            get { return _serviceName; }
            set
            {
                if (_serviceName != value)
                {
                    _serviceName = value;
                    OnPropertyChanged(nameof(name));
                }
            }
        }

        private string _serviceType;
        public string type
        {
            get { return _serviceType; }
            set
            {
                if (_serviceType != value)
                {
                    _serviceType = value;
                    OnPropertyChanged(nameof(type));
                }
            }
        }

        private decimal _servicePrice;
        public decimal price
        {
            get { return _servicePrice; }
            set
            {
                if (_servicePrice != value)
                {
                    _servicePrice = value;
                    OnPropertyChanged(nameof(price));
                }
            }
        }

        private TimeSpan _executionTime;
        public TimeSpan executionTime
        {
            get { return _executionTime; }
            set
            {
                if (_executionTime != value)
                {
                    _executionTime = value;
                    OnPropertyChanged(nameof(executionTime));
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
