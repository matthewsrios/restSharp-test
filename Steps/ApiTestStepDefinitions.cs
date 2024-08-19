using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using Newtonsoft.Json;

namespace rest_sharp_test.Steps
{
    [Binding]
    public class ApiTestSteps
    {
        private readonly IRestClient _client;
        private RestResponse _response;

        public ApiTestSteps()
        {
            _client = new RestClient("https://jsonplaceholder.typicode.com");
        }

        [Given(@"I have access to api")]
        public void GivenIHaveAccessToApi()
        { }

        [When(@"I send a GET request to (.*)")]
        public void WhenISendAGETRequestTo(string endpoint)
        {
            var request = new RestRequest(endpoint, Method.Get);
            _response = _client.Execute(request);
            Console.WriteLine(_response);
        }

        [Then(@"I see status (\d+)")]
        public void ThenISeeStatus(int expectedStatusCode)
        {
            Assert.That((int)_response.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Then(@"I see valid post data")]
        public void ThenISeeValidPostData()
        {
            Assert.IsNotNull(_response.Content);
            var post = JsonConvert.DeserializeObject<dynamic>(_response.Content);
            Assert.IsNotNull(post);
            var firstItem = post[0];
            Assert.IsNotNull(firstItem.userId, "userId is missing");
            Assert.IsNotNull(firstItem.id, "id is missing");
            Assert.IsNotNull(firstItem.title, "title is missing");
            Assert.IsNotNull(firstItem.body, "body is missing");
        }
    }
}
