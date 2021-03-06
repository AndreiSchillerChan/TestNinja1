using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja1.UnitTest
{
    [TestFixture]
    class BookingHelperTests
    {
        private Booking _booking;
        private Mock<IBookingRepository> _bookingHelper;

        [SetUp]
        public void SetUp()
        {
            _booking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };

            _bookingHelper = new Mock<IBookingRepository>();
            _bookingHelper.Setup(r => r.GetActiveBookings(1))
              .Returns(new List<Booking> { _booking }.AsQueryable());
        }


        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate, days : 2),
                DepartureDate = Before(_booking.ArrivalDate),
            }, _bookingHelper.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesInMiddleOfExistingBooking_ReturnExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate),
                DepartureDate = After(_booking.ArrivalDate),
            }, _bookingHelper.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsBeforeAndFinishesAfterExistingBooking_ReturnExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate),
            }, _bookingHelper.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAfterAndFinishesInMiddleOfExistingBooking_ReturnExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = Before(_booking.DepartureDate),
            }, _bookingHelper.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsInMiddleOfExistingBookingButFinishesAfter_ReturnExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate),
            }, _bookingHelper.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void OverlappingBookingsExist_BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.DepartureDate),
                DepartureDate = After(_booking.DepartureDate, days: 2),
            }, _bookingHelper.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void OverlappingBookingsExist_BookingsOverlapButBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate),
                Status = "Cancelled"
            }, _bookingHelper.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-1);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
