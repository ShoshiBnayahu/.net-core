namespace ToDo.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Interfaces;
public class tasksController : ControllerBase
{
public tasksController(){}
    

    [ApiController]
    [Route("[controller]")]
    public class TasksController : ControllerBase
    {
      readonly ITaskService TaskService;
       public TasksController(ITaskService taskService)
       {
           this.TaskService = taskService;
       }
       [HttpGet]
       public ActionResult<List<task>> GetAll() =>
               TaskService.GetAll();


       [HttpGet("{id}")]
       public ActionResult<task> GetById(int id)
       {
           var task = TaskService.GetById(id);

           if (task == null)
               return NotFound();

           return task;
       }

       [HttpPost]
       public IActionResult Create(task task)
       {
           TaskService.Add(task);
           return CreatedAtAction(nameof(Create), new { id = task.Id }, task);

       }

       [HttpPut("{id}")]
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
       public IActionResult Delete(int id)
       {
           var pizza = TaskService.GetById(id);
           if (pizza is null)
               return NotFound();

           TaskService.Delete(id);

           return NoContent();
       }
    }





























}
