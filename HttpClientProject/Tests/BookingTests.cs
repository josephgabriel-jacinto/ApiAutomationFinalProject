using HttpClientProject.DataModels;
using HttpClientProject.Helpers;
using HttpClientProject.Resources;
using HttpClientProject.Tests.TestData;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace HttpClientProject.Tests
{
    [TestClass]
    public class BookingTests
    {
        public HttpClient httpClient;
        List<OrderDetail> orderCleanupList = new List<OrderDetail>();
        
        [TestInitialize]
        public async Task Initialize()
        {
            httpClient = new HttpClient();

            Token token = await UserHelper.AuthenticateUser(httpClient);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Add("Cookie", $"token={token.TokenAuth}");
        }

        /// <summary>
        /// Removed created test data after each test method
        /// </summary>
        /// <returns></returns>
        [TestCleanup]
        public async Task Cleanup()
        {
            foreach (var data in orderCleanupList)
            {
                await httpClient.DeleteAsync(ApiEndpoint.DeleteBookingById(data.BookingId));
            }
        }

        /// <summary>
        /// Automated test to verify creation of Booking (POST method)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TC1_VerifyCreationOfBooking()
        {
            //Prepare data
            Booking booking = BookingData.NewBookingData();

            //Post Booking
            var postBooking = await BookingHelper.PostBooking(httpClient, booking);    
            var deserializedPostResponse = JsonConvert.DeserializeObject<OrderDetail>(postBooking.Content.ReadAsStringAsync().Result);

            orderCleanupList.Add(deserializedPostResponse);

            //Get Booking by ID
            var getResponse = await BookingHelper.GetBookingById(httpClient, deserializedPostResponse.BookingId);

            //Assertions
            Assert.AreEqual(HttpStatusCode.OK, postBooking.StatusCode, "POST method HttpStatus Code mismatched");
            Assert.AreEqual(booking.FirstName, getResponse.FirstName, "Firstname mismatched");
            Assert.AreEqual(booking.LastName, getResponse.LastName, "Lastname mismatched");
            Assert.AreEqual(booking.TotalPrice, getResponse.TotalPrice, "TotalPrice mismatched");
            Assert.AreEqual(booking.DepositPaid, getResponse.DepositPaid, "DepositPaid mismatched");
            Assert.AreEqual(booking.BookingDates.CheckIn, getResponse.BookingDates.CheckIn, "Checkin date mismatched");
            Assert.AreEqual(booking.BookingDates.CheckOut, getResponse.BookingDates.CheckOut, "CheckOut date mismatched");
            Assert.AreEqual(booking.AdditionalNeeds, getResponse.AdditionalNeeds, "AdditionalNeeds mismatched");
        }

        /// <summary>
        /// Automated test to verify updating of Booking (PUT method)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TC2_VerifyUpdateOfBooking()
        {
            //Prepare Data
            Booking booking = BookingData.NewBookingData();

            //Post Booking
            var postBooking = await BookingHelper.PostBooking(httpClient, booking);
            var deserializedPostResponse = JsonConvert.DeserializeObject<OrderDetail>(postBooking.Content.ReadAsStringAsync().Result);

            orderCleanupList.Add(deserializedPostResponse);

            //Update Firstname and Lastname of the created booking
            deserializedPostResponse.Booking.FirstName = "JoshUpdated";
            deserializedPostResponse.Booking.LastName = "GabUpdated";

            //Put booking request
            var putBookingResponse = await BookingHelper.PutBookingById(httpClient, deserializedPostResponse);
            
            //Get updated booking request
            var getUpdatedBooking = await BookingHelper.GetBookingById(httpClient, deserializedPostResponse.BookingId);

            //Assertions
            Assert.AreEqual(HttpStatusCode.OK, putBookingResponse.StatusCode, "PUT method HttpStatus Code mismatched");
            Assert.AreEqual(deserializedPostResponse.Booking.FirstName, getUpdatedBooking.FirstName, "Firstname mismatched");
            Assert.AreEqual(deserializedPostResponse.Booking.LastName, getUpdatedBooking.LastName, "Lastname mismatched");
        }


        /// <summary>
        /// Automated test to verify deletion of Booking (DELETE method)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TC3_VerifyDeletionOfBookingById()
        {
            //Prepare Data
            Booking booking = BookingData.NewBookingData();

            //Post Booking
            var postBooking = await BookingHelper.PostBooking(httpClient, booking);
            var deserializedPostResponse = JsonConvert.DeserializeObject<OrderDetail>(postBooking.Content.ReadAsStringAsync().Result);

            orderCleanupList.Add(deserializedPostResponse);

            //Delete Booking
            var deleteResponse = await BookingHelper.DeleteBookingById(httpClient, deserializedPostResponse.BookingId);

            //Assertions
            Assert.AreEqual(HttpStatusCode.Created, deleteResponse.StatusCode, "DELETE method HttpStatus Code mismatched");
        }

        /// <summary>
        /// Automated test to verify NEGATIVE scenario on retrieving of Booking (GET method)
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task TC4_VerifyGetInvalidBookingId()
        {
            //Prepare Data
            long invalidId = 1000091;

            //Get Booking invalid id
            var getResponse = await BookingHelper.GetBookingByInvalidId(httpClient, invalidId);

            //Assertions
            Assert.AreEqual(HttpStatusCode.NotFound, getResponse.StatusCode, "GET method HttpStatus Code mismatched for invalid id");
        }
    }
}