using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        
        public static string OverlappingBookingsExist(Booking booking, IBookingRepository bookingRepository)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = bookingRepository.GetActiveBookings(booking.Id);

            //Refactored following the repository pattern.
            //var unitOfWork = new UnitOfWork();
            //var bookings =
            //    unitOfWork.Query<Booking>()
            //        .Where(
            //            b => b.Id != booking.Id && b.Status != "Cancelled");

            //Didn't refactor the below because this is the logic of the method.
            var overlappingBooking =
                bookings.FirstOrDefault(
                    b => booking.ArrivalDate < b.DepartureDate &&
                            b.ArrivalDate < booking.DepartureDate);

                    //This below contains a bug:
                        //booking.ArrivalDate >= b.ArrivalDate
                        //&& booking.ArrivalDate < b.DepartureDate
                        //|| booking.DepartureDate > b.ArrivalDate
                        //&& booking.DepartureDate <= b.DepartureDate);

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public class UnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}