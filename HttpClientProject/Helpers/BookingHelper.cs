using HttpClientProject.DataModels;
using HttpClientProject.Resources;
using HttpClientProject.Tests.TestData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HttpClientProject.Helpers
{
    public class BookingHelper
    {
        public static async Task<HttpResponseMessage> PostBooking(HttpClient httpClient, Booking booking)
        {
            var serialized = JsonConvert.SerializeObject(booking);
            var request = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ApiEndpoint.PostBooking(), request);
                       
            return response;
        }

        public static async Task<Booking> GetBookingById(HttpClient httpClient, long id)
        {
            var getResponse = await httpClient.GetAsync(ApiEndpoint.GetBookingById(id));
            var deserializedResponse = JsonConvert.DeserializeObject<Booking>(getResponse.Content.ReadAsStringAsync().Result);

            return deserializedResponse;
        }

        public static async Task<HttpResponseMessage> GetBookingByInvalidId(HttpClient httpClient, long id)
        {
            var getResponse = await httpClient.GetAsync(ApiEndpoint.GetBookingById(id));

            return getResponse;
        }


        public static async Task<HttpResponseMessage> PutBookingById(HttpClient httpClient, OrderDetail orderDetails)
        {
            var serialized = JsonConvert.SerializeObject(orderDetails.Booking);
            var request = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync(ApiEndpoint.PutBookingById(orderDetails.BookingId), request);

            return response;
        }


        public static async Task<HttpResponseMessage> DeleteBookingById(HttpClient httpClient, long id)
        {
            var response = await httpClient.DeleteAsync(ApiEndpoint.DeleteBookingById(id));

            return response;
        }

    }
}
