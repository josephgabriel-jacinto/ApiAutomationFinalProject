using Newtonsoft.Json;
using RestSharp;
using RestSharpProject.DataModels;
using RestSharpProject.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Helpers
{
    public class UserHelper
    {
        /// <summary>
        /// Reusable Method for Authenticating User
        /// </summary>
        /// <param name="restClient"></param>
        /// <param name=""></param>
        /// <returns>Token</returns>
        public static async Task<Token> AuthenticateUser(RestClient restClient)
        {
            //Prepare User object with username and password
            User user = new User();
           
            //Prepare request
            var postRequest = new RestRequest(ApiEndpoint.AuthenticateUser());
            postRequest.AddHeader("Content-Type", "application/json");
            postRequest.AddJsonBody(user);

            //Execute POST request
            var postResponse = await restClient.ExecutePostAsync<Token>(postRequest);
            return JsonConvert.DeserializeObject<Token>(postResponse.Content);
        }
    }
}
