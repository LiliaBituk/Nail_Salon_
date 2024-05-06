using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Business_Logic
{
    public class Employee_Service
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        public virtual Employee Employee { get; set; }

        [ForeignKey("Employee")]
        public int idEmployee { get; set; }

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
