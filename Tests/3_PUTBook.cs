using System.Net.Http.Json;
using Bogus;
using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests;

public class PUTBook
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }

    [Test]
    public async Task TestPutBook()
    {
 
        Helper.TriggerRebuild();
        var book = new Book()
        {
            Title = new Faker().Random.Word(),
            Publisher = new Faker().Random.Word(),
            CoverImgUrl = new Faker().Random.Word(),
            BookId = 1
        };
        var sql =
            "insert into library.books (title, publisher, coverimgurl) VALUES (@title, @publisher, @coverImgUrl);";
        using (var conn = Helper.DataSource.OpenConnection())
        {
            conn.Execute(sql, book);
        }
        
        
        var response = await _httpClient.PutAsJsonAsync("http://localhost:5000/api/book/1", book);
        var responseObject = JsonConvert.DeserializeObject<Book>(
            await response.Content.ReadAsStringAsync());


        using (new AssertionScope())
        {
            responseObject.Should().BeEquivalentTo(book, Helper.MyBecause(responseObject, book));
            response.IsSuccessStatusCode.Should().BeTrue();
        }

    }
}