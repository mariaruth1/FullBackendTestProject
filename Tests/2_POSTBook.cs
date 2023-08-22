using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests;

public class POSTBook
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }

    [Test]
    public async Task PostBookTest()
    {
        Helper.TriggerRebuild();
        var input = new Book()
        {
            Title = "Mock book title",
            Publisher = "Mock book publisher",
            CoverImgUrl = "https://something.com/some.jpg"
        };
        var expected = new Book()
        {
            Title = input.Title,
            Publisher = input.Publisher,
            CoverImgUrl = input.CoverImgUrl,
            BookId = 1
        };
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/book", input);
        var responseObject = JsonConvert.DeserializeObject<Book>(
            await response.Content.ReadAsStringAsync());

        using (new AssertionScope())
        {
            responseObject.Should().BeEquivalentTo(expected, Helper.MyBecause(responseObject, expected));
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}