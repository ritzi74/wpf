using System;
using System.ComponentModel;

namespace NyaralasApp.Models
{
    public class Booking : INotifyPropertyChanged
    {
        private string _name;
        private DateTime _date;
        private decimal _amount;
        private int _bookingCount;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(nameof(Date)); }
        }

        public decimal Amount
        {
            get => _amount;
            set { _amount = value; OnPropertyChanged(nameof(Amount)); }
        }

        public int BookingCount
        {
            get => _bookingCount;
            set { _bookingCount = value; OnPropertyChanged(nameof(BookingCount)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Booking Clone()
        {
            return (Booking)MemberwiseClone();
        }
    }
}
