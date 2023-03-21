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
        public static async Task<Token> AuthenticateUser(RestClient restClient)
        {
            //Prepare User object with username and password
            User user = new User();
            Token token = new Token();

            //Prepare request
            var postRequest = new RestRequest(ApiEndpoint.AuthenticateUser());
            postRequest.AddHeader("Accept", "application/json");
            postRequest.AddJsonBody(user);

            //Execute POST request
            var postResponse = await restClient.ExecutePostAsync<Token>(postRequest);
            token.TokenAuth = postResponse.Data.TokenAuth;

            return token;
        }
    }
}
