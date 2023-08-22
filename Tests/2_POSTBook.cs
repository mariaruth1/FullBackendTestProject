using System.Net.Http.Json;
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
        var input = new
        {

        };
        _httpClient.PostAsJsonAsync("",input);
        
        
    }
}