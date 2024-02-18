using Microsoft.AspNetCore.Mvc;
 using ToDo.Models;
using ToDo.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class usersController : ControllerBase
    {
        readonly IUserService UserService;

        public usersController(IUserService UserService)
        {
            this.UserService = UserService;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public ActionResult<List<User>> GetAll() =>
                UserService.GetAll();


        [HttpPost]
        [Route("[action]")]
        [Authorize(Policy = "Admin")]
        public IActionResult Create([FromBody] User User)
        {
            UserService.Add(User);
            /////////////////////////////////מה זה עושה///////////////////////////////////
            return CreatedAtAction(nameof(Create), new { id = User.Id }, User);

        }

        //    [HttpGet("{id}")]
        //    public ActionResult<task> GetById(int id)
        //    {
        //        var task = TaskService.GetById(id);

        //        if (task == null)
        //            return NotFound();

        //        return task;
        //    }

        //    [HttpPost]
        //    public IActionResult Create(task task)
        //    {
        //        TaskService.Add(task);
        //        return CreatedAtAction(nameof(Create), new { id = task.Id }, task);

        //    }

        //    [HttpPut("{id}")]
        //    public IActionResult Update(int id, task task)
        //    {
        //        if (id != task.Id)
        //            return BadRequest();

        //        var existingTask = TaskService.GetById(id);
        //        if (existingTask is null)
        //            return NotFound();

        //        TaskService.Update(task);

        //        return NoContent();
        //    }

        //    [HttpDelete("{id}")]
        //    public IActionResult Delete(int id)
        //    {
        //        var pizza = TaskService.GetById(id);
        //        if (pizza is null)
        //            return NotFound();

        //        TaskService.Delete(id);

        //        return NoContent();
        //    }

        

    }

}



