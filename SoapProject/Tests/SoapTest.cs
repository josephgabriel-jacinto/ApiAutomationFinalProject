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


        private List<tCountryCodeAndName> getCountryNamesByCode()
        {
            List<tCountryCodeAndName> listOfCountryNames = countryInfoClient.ListOfCountryNamesByCode();
            return listOfCountryNames;
        }

        private tCountryCodeAndName getRandomCountry(List<tCountryCodeAndName> listOfCountryNames)
        {
            Random randonmizer = new Random();
            int index = randonmizer.Next(listOfCountryNames.Count);
            return listOfCountryNames[index];
        }


    }
}