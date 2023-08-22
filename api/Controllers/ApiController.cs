using Microsoft.AspNetCore.Mvc;
using service;

namespace FullBackendTestProject.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    private readonly Service _service;

    public ApiController(Service service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("/api/books")]
    public object GetBooks()
    {
        return _service.GetAllBooks();
    }


}
