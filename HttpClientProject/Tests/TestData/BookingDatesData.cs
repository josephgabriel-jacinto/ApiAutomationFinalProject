using HttpClientProject.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientProject.Tests.TestData
{
    public class BookingDatesData
    {
        public static BookingDate NewBookingDates() 
        {
            return new BookingDate
            {
                CheckIn = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                CheckOut = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))
            };
        }

    }
}
