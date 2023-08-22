using Bogus;
using Dapper;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace Tests;

public class DELETEBook
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
    }

    [Test]
    public async Task DeleteBookTest()
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


        var response = await _httpClient.DeleteAsync("http://localhost:5000/api/book/1");


        using (new AssertionScope())
        {
            using (var conn = Helper.DataSource.OpenConnection())
            {
                (conn.ExecuteScalar<int>("SELECT COUNT(*) FROM library.books WHERE bookId = 1;", book) == 0)
                    .Should()
                    .BeTrue();
            }

            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
}