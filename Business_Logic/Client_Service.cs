using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Logic
{
    public class Client_Service : INotifyPropertyChanged
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        public virtual Customer Customer { get; set; }

        [ForeignKey("Customer")]
        public int idCustomer { get; set; }

        public virtual Service Service { get; set; }

        [ForeignKey("Service")]
        public int idService { get; set; }

        private DateTime _dateTime;
        public DateTime dateTime
        {
            get { return _dateTime; }
            set
            {
                if (_dateTime != value)
                {
                    _dateTime = value;
                    OnPropertyChanged(nameof(dateTime));
                }
            }
        }

        private DateTime _endTime;
        public DateTime endTime
        {
            get { return _endTime; }
            set
            {
                if (_endTime != value)
                {
                    _endTime = value;
                    OnPropertyChanged(nameof(endTime));
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
