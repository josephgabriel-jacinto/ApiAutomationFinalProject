using RestSharp;
using RestSharpProject.DataModels;
using RestSharpProject.Helpers;
using RestSharpProject.Resources;
using RestSharpProject.Tests.GenerateData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Tests
{
    [TestClass]
    public class BookingTests : BaseBookingTest
    {
        List<BookingSummary> bookingSummaryCleanupList = new List<BookingSummary>();

        [TestInitialize]
        public async Task TestInitialize()
        {
       
        }

        /// <summary>
        /// Removed created test data after each test method
        /// </summary>
        /// <returns></returns>
        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in bookingSummaryCleanupList)
            {
                await restClient.DeleteAsync(new RestRequest(ApiEndpoint.DeleteBookingById(data.BookingId)));
            }
        }

        /// <summary>
        /// Automated test to verify creation of Booking (POST method)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TC1_VerifyCreateBooking()
        {
            //prepare data
            Booking booking = BookingData.NewBookingData();

            //Execute Post request method
            var postResponse = await BookingHelper.PostBooking(restClient, booking);

            //add to cleanup list
            bookingSummaryCleanupList.Add(postResponse.Data);

            //Execute Get request method
            var getResponse = await BookingHelper.GetBookingById(restClient, postResponse.Data.BookingId);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, postResponse.StatusCode, "POST method HttpStatus Code mismatched");
            Assert.AreEqual(booking.FirstName, getResponse.Data.FirstName, "Firstname mismatched");
            Assert.AreEqual(booking.LastName, getResponse.Data.LastName, "Lastname mismatched");
            Assert.AreEqual(booking.TotalPrice, getResponse.Data.TotalPrice, "TotalPrice mismatched");
            Assert.AreEqual(booking.DepositPaid, getResponse.Data.DepositPaid, "DepositPaid mismatched");
            Assert.AreEqual(booking.BookingDates.CheckIn, getResponse.Data.BookingDates.CheckIn, "Checkin date mismatched");
            Assert.AreEqual(booking.BookingDates.CheckOut, getResponse.Data.BookingDates.CheckOut, "CheckOut date mismatched");
            Assert.AreEqual(booking.AdditionalNeeds, getResponse.Data.AdditionalNeeds, "AdditionalNeeds mismatched");
        }

        /// <summary>
        /// Automated test to verify updating of Booking (PUT method)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TC2_VerifyUpdateBooking()
        {
            //prepare data
            token = await UserHelper.AuthenticateUser(restClient);
            Booking booking = BookingData.NewBookingData();

            //Execute Post request method
            var postResponse = await BookingHelper.PostBooking(restClient, booking);

            //add to cleanup list
            bookingSummaryCleanupList.Add(postResponse.Data);

            //Update Firstname and Lastname
            postResponse.Data.Booking.FirstName = "JoshUpdated";
            postResponse.Data.Booking.LastName = "GabUpdated";

            //Execute Put request method
            var putResponse = await BookingHelper.PutBooking(restClient, postResponse.Data, token);

            //Execute Get request method
            var getUpdatedResponse = await BookingHelper.GetBookingById(restClient, postResponse.Data.BookingId);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, putResponse.StatusCode, "PUT method HttpStatus Code mismatched");
            Assert.AreEqual(postResponse.Data.Booking.FirstName, getUpdatedResponse.Data.FirstName, "Firstname mismatched");
            Assert.AreEqual(postResponse.Data.Booking.LastName, getUpdatedResponse.Data.LastName, "Lastname mismatched");
        }

        /// <summary>
        /// Automated test to verify deletion of Booking (DELETE method)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TC3_VerifyDeleteBooking()
        {
            //prepare data
            token = await UserHelper.AuthenticateUser(restClient);
            Booking booking = BookingData.NewBookingData();

            //Execute Post request method
            var postResponse = await BookingHelper.PostBooking(restClient, booking);

            //add to cleanup list
            bookingSummaryCleanupList.Add(postResponse.Data);

            //Execute Get request method
            var deleteResponse = await BookingHelper.DeleteBookingById(restClient, postResponse.Data.BookingId, token);

            //Assert
            Assert.AreEqual(HttpStatusCode.Created, deleteResponse.StatusCode, "DELETE method HttpStatus Code mismatched");
        }

        /// <summary>
        /// Automated test to verify NEGATIVE scenario on retrieving of Booking (GET method)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TC4_VerifyRetrieveBookingInvalidId()
        {
            //prepare data
            long invalidId = 1000091;

            //Execute Get request method
            var getResponse = await BookingHelper.GetBookingById(restClient, invalidId);

            //Assertions
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode, "GET method HttpStatus Code mismatched for invalid id");
        }
    }
}
