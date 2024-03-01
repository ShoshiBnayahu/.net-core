
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace ToDo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        readonly ITaskService TaskService;
        private readonly int userId;

        public ToDoController(ITaskService taskService ,IHttpContextAccessor httpContextAccessor)
        {
            this.TaskService = taskService;
            this.userId = int.Parse(httpContextAccessor?.HttpContext?.User.FindFirst("Id")?.Value);
        }

        [HttpGet]
        [Authorize(Policy = "User")]
        public ActionResult<List<task>> GetAll() =>
                      TaskService.GetAll(this.userId);

        [HttpGet("{id}")]
        [Authorize(Policy = "User")]

       public ActionResult<task> GetById(int id)=>TaskService.GetById(id);


            [HttpPost]
            [Authorize(Policy = "User")]
        public IActionResult Create(task task)
           {
                TaskService.Add(task,this.userId);
               return CreatedAtAction(nameof(Create), new { id = task.Id }, task);

           }

          [HttpPut("{id}")]
          [Authorize(Policy = "User")]
           public IActionResult Update(int id, task task)
           {
               if (id != task.Id)
                   return BadRequest();
               var existingTask = TaskService.GetById(id);
               if (existingTask is null)
                   return NotFound();  
               TaskService.Update(task);
               return NoContent();
           }

           [HttpDelete("{id}")]
           [Authorize(Policy = "User")]
           public IActionResult Delete(int id)
           {
              var existingTask = TaskService.GetById(id);
               if (existingTask is null)
                   return NotFound();

               TaskService.Delete(id);

               return NoContent();
           }
    }
}

