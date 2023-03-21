using HttpClientProject.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientProject.Tests.TestData
{
    public class BookingData
    {
        public static Booking NewBookingData() { 
            return new Booking
            {
                FirstName = "Josh",
                LastName = "Gab",
                TotalPrice = 100,
                DepositPaid = true,
                BookingDates = BookingDatesData.NewBookingDates(),
                AdditionalNeeds = "Straw"
            }; 
        }
    }
}
