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
        throw new NotImplementedException();
    }

    [HttpPut]
    [Route("/api/book/{bookId}")]
    public Book UpdateBook([FromBody] Book book, [FromRoute] int bookId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Route("/api/book/{bookId}")]
    public object DeleteBook([FromRoute] int bookId)
    {
        throw new NotImplementedException();
    }


}
