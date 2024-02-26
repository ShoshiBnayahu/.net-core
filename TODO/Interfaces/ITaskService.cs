using ToDo.Models;
using System.Collections.Generic;


namespace ToDo.Interfaces;

public  interface ITaskService
{

    List<task> GetAll(int userId) ;
    task GetById(int id) ;
    void Add(task newTask,int userId);
    void Update(task newTask);
    void Delete(int id);
}