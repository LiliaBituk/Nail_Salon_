using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business_Logic
{
    public class Customer : INotifyPropertyChanged
    {
        [Key]
        public int id { get; set; }

        private string _fullName;
        public string fullName
        {
            get { return _fullName; }
            set
            {
                if (_fullName != value)
                {
                    _fullName = value;
                    OnPropertyChanged(nameof(fullName));
                }
            }
        }

        private DateTime? _birthDate;
        public DateTime? birthDate
        {
            get { return _birthDate; }
            set
            {
                if (_birthDate != value)
                {
                    _birthDate = value;
                    OnPropertyChanged(nameof(birthDate));
                }
            }
        }

        private decimal _phoneNumber;
        public decimal phoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (_phoneNumber != value)
                {
                    _phoneNumber = value;
                    OnPropertyChanged(nameof(phoneNumber));
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
