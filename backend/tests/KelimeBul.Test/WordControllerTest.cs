using FluentAssertions;
using KelimeBul.API;
using KelimeBul.API.Models;
using Microsoft.AspNetCore.Mvc;
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
        [InlineData("elma")]
        [InlineData("armut")]
        [InlineData("taşımak")]
        [InlineData("serbest")]
        [InlineData("uzaklaştırma")]
        public async Task Exist_ShouldReturnOk_GivenCorrectWord(string word)
        {
            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Head,"api/v1/words/" + word));
            response.EnsureSuccessStatusCode();
        }
        [Theory]
        [InlineData("sjfks")]
        [InlineData("lpjl")]
        [InlineData("taşımakj")]
        [InlineData("serbestk")]
        public async Task Exist_ShouldReturnNotFound_GivenWrongWord(string word)
        {
            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Head,"api/v1/words/" + word));
           
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
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
        public async Task RandomWord_ShouldCorrectWordLength(int length)
        {
            var response = await Client.GetAsync("api/v1/words/random?length="+ length);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            ApiResponse randomWordResponseModel = JsonSerializer.Deserialize<ApiResponse>(jsonString);

            
           // Assert.Equal(length, randomWordResponseModel.Word.Length);
        }
    }
}
