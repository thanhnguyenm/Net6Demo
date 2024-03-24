using DomainEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private static readonly User[] Users = new[]
        {
            new User{ Id = Guid.NewGuid(), Firstname = "Janle", Lastname = "Kim", Email = "janle@yopmail.com" },
            new User{ Id = Guid.NewGuid(), Firstname = "Tom", Lastname = "Crise", Email = "Tom@yopmail.com" },
            new User{ Id = Guid.NewGuid(), Firstname = "Ronaldo", Lastname = "Critona", Email = "Ronaldo@yopmail.com" },
            new User{ Id = Guid.NewGuid(), Firstname = "Lovedde", Lastname = "Anloid", Email = "Lovedde@yopmail.com" },
            new User{ Id = Guid.NewGuid(), Firstname = "Kasdd", Lastname = "Mghas", Email = "Kasdd@yopmail.com" }
        };

        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(401)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WeatherForecast>))]
        public IEnumerable<User> Get()
        {
            return Users;
        }
    }
}