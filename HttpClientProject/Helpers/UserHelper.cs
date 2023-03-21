using HttpClientProject.DataModels;
using HttpClientProject.Resources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientProject.Helpers
{
    public class UserHelper
    {
        public static async Task<Token> AuthenticateUser(HttpClient httpClient)
        {
            User user = new User();
            var serialized = JsonConvert.SerializeObject(user);
            var request = new StringContent(serialized, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ApiEndpoint.AuthenticateUser(), request);

            //set retrieved pet to petRetrived variable
            var deserialized = JsonConvert.DeserializeObject<Token>(response.Content.ReadAsStringAsync().Result);

            Token token = new Token();
            token.TokenAuth = deserialized.TokenAuth;

            return token;
        }
    }
}
