using Dapper;
using infrastructure.DataModels;
using Npgsql;

namespace infrastructure;

public class Repository
{
    private readonly NpgsqlDataSource _dataSource;

    public Repository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        var sql = $@"select * from library.books;";
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.Query<Book>(sql);
        }
    }


    public Book CreateBook(Book book)
    {
        var sql = $@"INSERT INTO library.books (title, publisher, coverimgurl) 
VALUES (@title, @publisher, @coverImgUrl) RETURNING *;";
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<Book>(sql, new {book.Title, book.Publisher, book.CoverImgUrl});
        }
    }
    
    public Book UpdateBook(Book book, int bookId)
    {
        var sql = $@"UPDATE library.books
SET title = @title, publisher = @publisher, coverimgurl = @coverImgUrl 
WHERE bookid = @bookId RETURNING bookId, title, publisher, coverImgUrl;";
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.QuerySingle<Book>(sql, new {book.Title, book.Publisher, book.CoverImgUrl, bookId});
        }
    }
    
    public bool DeleteBook(int bookId)
    {
        var sql = $@"DELETE FROM library.books WHERE bookid = @bookId;";
        using(var conn = _dataSource.OpenConnection())
        {
            return conn.Execute(sql, new {bookId}) == 1;
        }
    }
}