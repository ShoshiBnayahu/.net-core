namespace ToDo.Services;
using ToDo.Models;
using ToDo.Interfaces;
using System.Text.Json;

public  class TaskService : ITaskService
{
List<task> tasks;                                
private string fileName = "Task.json";
public TaskService(IWebHostEnvironment webHost)
       {
           this.fileName = Path.Combine(webHost.ContentRootPath, "Data", "Task.json");

           using (var jsonFile = File.OpenText(fileName))
           {
           tasks = JsonSerializer.Deserialize<List<task>>(jsonFile.ReadToEnd(),
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
});
       }
}
       private void saveToFile()
       {
           File.WriteAllText(fileName, JsonSerializer.Serialize(tasks));
       }
    
   public List<task> GetAll(int userId) => tasks.Where(t=>t.UserId==userId).ToList();

  

   public  task GetById(int id) => tasks.FirstOrDefault(t=>t.Id==id);


    public void Add(task task,int userId)
       {
           task.Id = tasks.Max(t=>t.Id)+1;
           task.UserId=userId;
           tasks.Add(task);
           saveToFile();
       }

   public void Update(task task)
       {
           var index = tasks.FindIndex(p => p.Id == task.Id);
           if (index == -1)
               return;
           tasks[index].Name = task.Name;
           tasks[index].IsDone = task.IsDone;

           saveToFile();
       }


       public void Delete(int id)
       {
           var task = GetById(id);
           if (task is null)
               return;
           tasks.Remove(task);
           saveToFile();
       }

   }

