using RestSharp;
using RestSharpProject.DataModels;
using RestSharpProject.Resources;
using RestSharpProject.Tests.GenerateData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Helpers
{
    public class BookingHelper
    {
        /// <summary>
        /// Reusable Method for creating Booking
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="booking"></param>
        /// <returns></returns>
        public static async Task<RestResponse<BookingSummary>> PostBooking(RestClient restClient, Booking booking)
        {
            //prepare Post request
            var postRequest = new RestRequest(ApiEndpoint.PostBooking());
            postRequest.AddHeader("Accept", "application/json");
            postRequest.AddOrUpdateHeader("ContentType", "application/json");
            postRequest.AddJsonBody(booking, ContentType.Json);

            //execute request
            return await restClient.ExecutePostAsync<BookingSummary>(postRequest);
        }

        /// <summary>
        /// Reusable Method for updating Booking
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="bookingSummary"></param>
        /// <returns></returns>
        public static async Task<RestResponse> PutBooking(RestClient restClient, BookingSummary bookingSummary, Token token)
        {
            //prepare request
            var putRequest = new RestRequest(ApiEndpoint.PutBookingById(bookingSummary.BookingId));
            putRequest.AddHeader("Accept", "application/json");
            putRequest.AddHeader("Cookie", $"token={token}");
            putRequest.AddJsonBody(bookingSummary.Booking);

            //execute request
            var putResponse = await restClient.ExecutePutAsync<RestResponse>(putRequest);

            return putResponse;
        }

        /// <summary>
        /// Reusable Method for retrieving Booking By ID
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<RestResponse<Booking>> GetBookingById(RestClient restClient, long id)
        {
            //prepare request
            var getRequest = new RestRequest(ApiEndpoint.GetBookingById(id));

            //execute request
            var getResponse = await restClient.ExecuteGetAsync<Booking>(getRequest);

            return getResponse;
        }

        /// <summary>
        /// Reusable Method for deleting Booking by ID
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<RestResponse> DeleteBookingById(RestClient restClient, long id, Token token)
        {
            //prepare request
            var deleteRequest = new RestRequest(ApiEndpoint.DeleteBookingById(id));
            deleteRequest.AddHeader("Cookie", $"token={token}");

            //execute request
            var getResponse = await restClient.DeleteAsync(deleteRequest);

            return getResponse;
        }

    }
}
