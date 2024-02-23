
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
        readonly ITaskService TaskService;
        private readonly int userId;

        public TasksController(ITaskService taskService ,IHttpContextAccessor httpContextAccessor)
        {
            this.TaskService = taskService;
            this.userId = int.Parse(httpContextAccessor?.HttpContext?.User.FindFirst("Id")?.Value);
        }

        // [HttpGet]
        // [Authorize(Policy = "Admin")]
        // public ActionResult<List<task>> GetAll() =>
        //         TaskService.GetAll();


   

    [HttpGet("{id}")]
    [Authorize(Policy = "Admin")]
    [Authorize(Policy = "User")]

    public ActionResult<task> GetById(int ? id=this.userId)
      {
                var task = TaskService.GetById(id);

                if (task == null)
                    return NotFound();

                return task;
      }

//     [HttpGet]
// [Route("GetById")]
//     [Authorize(Policy = "User")]
//     public ActionResult<task> GetById()
//       {
//                 var task = TaskService.GetById(this.userId);

//                 if (task == null)
//                     return NotFound();

//                 return task;
//       }


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

