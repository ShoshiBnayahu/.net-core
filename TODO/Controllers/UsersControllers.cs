using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy = "User")]
    public class usersController : ControllerBase
    {
        readonly IUserService UserService;
        private readonly int userId;


        public usersController(IUserService UserService, IHttpContextAccessor httpContextAccessor)
        {
            this.UserService = UserService;
            this.userId = int.Parse(httpContextAccessor?.HttpContext?.User.FindFirst("Id")?.Value);

        }
        [HttpGet]
        [Route("currentUser")]
        public ActionResult<User> getUser() =>
                    UserService.GetById(this.userId);

        [HttpPut]
        [Route("currentUser")]
        public ActionResult updateCurrentUser(User user)
        {
            if (this.userId != user.Id)
                return BadRequest();
            UserService.Update(user);
            return NoContent();
        }


        [HttpGet]
        [Authorize(Policy = "Admin")]
        public ActionResult<List<User>> GetAll() =>
                     UserService.GetAll();


        [HttpGet("{id}")]
        [Authorize(Policy = "Admin")]
        public ActionResult<User> GetById(int id) => UserService.GetById(id);

        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id)
        {
            var existingTask = UserService.GetById(id);
            if (existingTask is null)
                return NotFound();

            UserService.Delete(id);
            return NoContent();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public IActionResult Create(User user)
        {
            UserService.Add(user);
            return CreatedAtAction(nameof(Create), new { id = user.Id }, user);

        }


    }

}



