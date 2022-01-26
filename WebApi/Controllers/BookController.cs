using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [ApiController] // api controller olduğunu belirt
    [Route("[controller]s")] // site/BookController/endpoint + s şeklinde ulaşılacak
    public class BookController : ControllerBase
    {
        
    }
}