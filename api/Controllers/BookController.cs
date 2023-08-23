using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service;

namespace FullBackendTestProject.Controllers;

[ApiController]
public class BookController : ControllerBase
{
    private readonly Service _service;

    public BookController(Service service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/api/books")]
    public IEnumerable<Book> GetBooks()
    {
        return _service.GetAllBooks();
    }

    [HttpPost]
    [Route("/api/book")]
    public Book PostBook([FromBody]Book book)
    {
        return _service.CreateBook(book);
    }

    [HttpPut]
    [Route("/api/book/{bookId}")]
    public Book UpdateBook([FromBody] Book book, [FromRoute] int bookId)
    {
        return _service.UpdateBook(book, bookId);
    }

    [HttpDelete]
    [Route("/api/book/{bookId}")]
    public bool DeleteBook([FromRoute] int bookId)
    {
        return _service.DeleteBook(bookId);
    }


}
