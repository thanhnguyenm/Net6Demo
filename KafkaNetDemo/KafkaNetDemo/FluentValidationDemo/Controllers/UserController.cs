using Microsoft.AspNetCore.Mvc;

namespace FluentValidationDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager userManager;

        public UserController(UserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            await userManager.Validate(new FluentValidationDemo.User { Name = name});
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User model)
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}
