using Newtonsoft.Json;
using RestSharp;
using RestSharpProject.DataModels;
using RestSharpProject.Resources;
using RestSharpProject.Tests.GenerateData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
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
        /// <returns>Task<RestResponse<BookingSummary>></returns>
        public static async Task<RestResponse<BookingSummary>> PostBooking(RestClient restClient, Booking booking)
        {
            //prepare Post request
            var postRequest = new RestRequest(ApiEndpoint.PostBooking(), Method.Post);
            postRequest.AddHeader("Accept", "application/json");
            postRequest.AddHeader("Content-Type", "application/json");
            postRequest.AddJsonBody(JsonConvert.SerializeObject(booking));

            //execute request
            return await restClient.ExecutePostAsync<BookingSummary>(postRequest);
        }

        /// <summary>
        /// Reusable Method for updating Booking
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="bookingSummary"></param>
        /// <returns>ask<RestResponse></returns>
        public static async Task<RestResponse> PutBooking(RestClient restClient, BookingSummary bookingSummary)
        {
            //prepare request
            var putRequest = new RestRequest(ApiEndpoint.PutBookingById(bookingSummary.BookingId));
            putRequest.AddHeader("Accept", "application/json");
            putRequest.AddHeader("Content-Type", "application/json");
            putRequest.AddJsonBody(JsonConvert.SerializeObject(bookingSummary.Booking));
            
            //execute request
            return await restClient.ExecutePutAsync<RestResponse>(putRequest);
        }

        /// <summary>
        /// Reusable Method for retrieving Booking By ID
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="id"></param>
        /// <returns>Task<RestResponse<Booking>></returns>
        public static async Task<RestResponse<Booking>> GetBookingById(RestClient restClient, long id)
        {
            //prepare request
            var getRequest = new RestRequest(ApiEndpoint.GetBookingById(id));
            getRequest.AddHeader("Accept", "application/json");
            
            //execute request
            return await restClient.ExecuteGetAsync<Booking>(getRequest);
        }

        /// <summary>
        /// Reusable Method for deleting Booking by ID
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns>Task<RestResponse></returns>
        public static async Task<RestResponse> DeleteBookingById(RestClient restClient, long id, Token token)
        {
            //prepare request
            var deleteRequest = new RestRequest(ApiEndpoint.DeleteBookingById(id));
            deleteRequest.AddHeader("Content-Type", "application/json");
            
            //execute request
            return await restClient.DeleteAsync(deleteRequest);
        }

    }
}
