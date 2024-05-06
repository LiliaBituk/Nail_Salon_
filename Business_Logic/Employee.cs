using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business_Logic
{
    public class Employee : INotifyPropertyChanged
    {
        [Key]
        public int id { get; set; }

        private string _employeeName;
        public string fullName
        {
            get { return _employeeName; }
            set
            {
                _employeeName = value;
                OnPropertyChanged(nameof(fullName));
            }
        }

        private string _employmentContractNumber;
        public string employmentContractNumber
        {
            get { return _employmentContractNumber; }
            set
            {
                _employmentContractNumber = value;
                OnPropertyChanged(nameof(employmentContractNumber));
            }
        }

        private DateTime _birthDate;
        public DateTime birthDate
        {
            get { return _birthDate; }
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(birthDate));
            }
        }

        private string _typeService;
        public string typeService
        {
            get { return _typeService; }
            set
            {
                _typeService = value;
                OnPropertyChanged(nameof(typeService));

            }
        }

        private bool _permanentEmployee;
        public bool permanentEmployee
        {
            get { return _permanentEmployee; }
            set
            {
                _permanentEmployee = value;
                OnPropertyChanged(nameof(permanentEmployee));

            }
        }

        private decimal _phoneNumber;
        public decimal phoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(phoneNumber));

            }
        }

        private int _score;
        public int score
        {
            get { return _score; }
            set
            {

                _score = value;
                OnPropertyChanged(nameof(score));

            }
        }

        public ICollection<Employee_Service> Employee_Service { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
