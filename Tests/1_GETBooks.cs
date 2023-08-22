using System.Net;
using Dapper;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests;

public class GETBooks
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }

    [Test]
    public async Task GetAllBooksTest()
    {
        //Arrange
        Helper.TriggerRebuild();
        var expected = new List<Book>();
        for (var i = 1; i < 10; i++)
        {

            var book = Helper.MakeRandomBookWithId(i);
            expected.Add(book);
            //Note if you're reading this: There is a more performant way of making "bulk" inserts rather than loops,
            //but since this is simply 10 inserts, it's "good enough"
            var sql = $@" 
            insert into library.books (title, publisher, coverimgurl) VALUES (@title, @publisher, @coverImgUrl);
            ";
            using (var conn = Helper.DataSource.OpenConnection())
            {
                conn.Execute(sql, book);
            }
        }
        var expeectedStatusCode = HttpStatusCode.OK;
        
        //Act
        var response = await _httpClient.GetAsync("http://localhost:5000/api/books");
        var content = await response.Content.ReadAsStringAsync();
        var actualBooks = JsonConvert.DeserializeObject<IEnumerable<Book>>(content)!;
        
        //ASSERT
        actualBooks.Should().BeEquivalentTo(expected);
        response.StatusCode.Should().Be(expeectedStatusCode);


    }
}