using RestSharpProject.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Tests.GenerateData
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
