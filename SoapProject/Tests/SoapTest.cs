using ServiceReference1;

namespace SoapProject.Tests
{
    [TestClass]
    public class SoapTest
    {
        CountryInfoServiceSoapTypeClient countryInfoClient;

        [TestInitialize]
        public void TestInitialize()
        {
            countryInfoClient = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        /// <summary>
        /// Test to verify Country record correctness
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TC1_VerifyFullCountryInfoMethod()
        {
            //get country list
            List<tCountryCodeAndName> countryList = this.getCountryNamesByCode();

            //get random record
            tCountryCodeAndName randomCountry = this.getRandomCountry(countryList);

            tCountryInfo countryInfo = countryInfoClient.FullCountryInfo(randomCountry.sISOCode);

            Assert.IsTrue(randomCountry.sISOCode.Equals(countryInfo.sISOCode), "Country code mismatched");
            Assert.IsTrue(randomCountry.sName.Equals(countryInfo.sName), "Country name mismatched");
        }

        /// <summary>
        /// Test to verify Country ISO Code correctness using random records
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public void TC1_VerifyCountryISOCodeMethod()
        {
            int max = 5;
            List<string> countryISOCodeList = new List<string>();
            List<tCountryCodeAndName> countryList = this.getCountryNamesByCode();

            //get 5 random country
            List<tCountryCodeAndName> randomCountries = new List<tCountryCodeAndName>();

            for (int i = 0; i < max; i++)
            {
                tCountryCodeAndName randomCountry = this.getRandomCountry(countryList);

                if (i == 0)
                {
                    randomCountries.Add(randomCountry);
                }
                else
                {
                    if (randomCountries.Contains(randomCountry))
                    {
                        i = i - 1;
                    }
                    else
                    {
                        randomCountries.Add(randomCountry);
                    }
                }
            }

            foreach (tCountryCodeAndName country in randomCountries)
                countryISOCodeList.Add(countryInfoClient.CountryISOCode(country.sName));


            //Assert
            for (int ctr = 0; ctr < countryISOCodeList.Count; ctr++)
                Assert.IsTrue(randomCountries[ctr].sISOCode.Equals(countryISOCodeList[ctr]), "Country code mismatched");
        }

        /// <summary>
        /// Private method for retrieving the Country records list
        /// </summary>
        /// <returns>List<tCountryCodeAndName></returns>
        private List<tCountryCodeAndName> getCountryNamesByCode()
        {
            List<tCountryCodeAndName> listOfCountryNames = countryInfoClient.ListOfCountryNamesByCode();
            return listOfCountryNames;
        }

        /// <summary>
        /// Private method for getting random Country record
        /// </summary>
        /// <returns>tCountryCodeAndName</returns>
        private tCountryCodeAndName getRandomCountry(List<tCountryCodeAndName> listOfCountryNames)
        {
            Random randonmizer = new Random();
            int index = randonmizer.Next(listOfCountryNames.Count);
            return listOfCountryNames[index];
        }


    }
}