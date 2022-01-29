using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateUser;
using WebApi.Application.UserOperations.CreateToken;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        // appsettings içindeki verilere ulaşmanı sağlıyor
        private readonly IConfiguration _configuration;

        public UserController(IMapper mapper, IBookStoreDbContext context, IConfiguration configuration)
        {
            _mapper = mapper;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_mapper, _context);
            command.Model = newUser;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_mapper, _context, _configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }
    }
}