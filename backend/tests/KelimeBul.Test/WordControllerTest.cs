using KelimeBul.API;
using KelimeBul.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace KelimeBul.Test
{
    [Collection("Sequential")]
    public class WordControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        public HttpClient Client { get; }
        public WordControllerTest(WebApplicationFactory<Startup> factory)
        {
            Client = factory.CreateClient();
        }



        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        public async Task ReturnsTrueAvailableRandomLengthWord(int length)
        {
            var response = await Client.GetAsync("api/v1/words/random?length="+ length);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            RandomWordResponseModel randomWordResponseModel = JsonSerializer.Deserialize<RandomWordResponseModel>(jsonString);

            
            Assert.Equal(length, randomWordResponseModel.Word.Length);
        }
    }
}
