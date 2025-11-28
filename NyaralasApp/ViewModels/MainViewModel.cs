using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using NyaralasApp.Models;
using NyaralasApp.Views;

namespace NyaralasApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Booking> Bookings { get; } =
            new ObservableCollection<Booking>();

        private Booking _selectedBooking;
        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set { _selectedBooking = value; OnPropertyChanged(); }
        }

        public ICommand AddBookingCommand { get; }
        public ICommand EditBookingCommand { get; }
        public ICommand DeleteBookingCommand { get; }

        public MainViewModel()
        {
            // Tesztadatok
            Bookings.Add(new Booking
            {
                Name = "Kiss Béla",
                Date = DateTime.Today.AddDays(7),
                Amount = 120000,
                BookingCount = 7
            });

            AddBookingCommand = new RelayCommand(_ => AddBooking());
            EditBookingCommand = new RelayCommand(_ => EditBooking(),
                                                    _ => SelectedBooking != null);
            DeleteBookingCommand = new RelayCommand(_ => DeleteBooking(),
                                                    _ => SelectedBooking != null);
        }

        private void AddBooking()
        {
            var booking = new Booking
            {
                Date = DateTime.Today.AddDays(1),
                BookingCount = 1
            };

            var editor = new BookingEditorWindow { DataContext = booking };
            bool? result = editor.ShowDialog();

            if (result == true && Validate(booking))
                Bookings.Add(booking);
        }

        private void EditBooking()
        {
            var copy = SelectedBooking.Clone();
            var editor = new BookingEditorWindow { DataContext = copy };
            bool? result = editor.ShowDialog();

            if (result == true && Validate(copy))
            {
                SelectedBooking.Name = copy.Name;
                SelectedBooking.Date = copy.Date;
                SelectedBooking.Amount = copy.Amount;
                SelectedBooking.BookingCount = copy.BookingCount;
            }
        }

        private void DeleteBooking()
        {
            if (SelectedBooking == null) return;

            if (MessageBox.Show("Biztosan törlöd a foglalást?",
                                "Törlés",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Bookings.Remove(SelectedBooking);
            }
        }

        private bool Validate(Booking b)
        {
            if (string.IsNullOrWhiteSpace(b.Name))
            {
                MessageBox.Show("A név nem lehet üres.");
                return false;
            }

            if (b.Date < DateTime.Today)
            {
                MessageBox.Show("A dátum nem lehet múltbeli.");
                return false;
            }

            if (b.Amount <= 0 || b.BookingCount <= 0)
            {
                MessageBox.Show("A költség és az éjszakák száma legyen pozitív.");
                return false;
            }

            return true;
        }
    }
}
