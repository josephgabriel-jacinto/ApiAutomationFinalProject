using RestSharp;
using RestSharpProject.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpProject.Tests
{
    public class BaseBookingTest
    {
        public RestClient restClient { get; set; }
        public Token token = new Token();

        [TestInitialize]
        public void Initialize()
        {
            restClient = new RestClient();
        }

    }
}
