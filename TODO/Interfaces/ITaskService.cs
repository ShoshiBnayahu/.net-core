using ToDo.Models;
using System.Collections.Generic;


namespace ToDo.Interfaces;

public  interface ITaskService
{

    List<task> GetAll() ;

     task GetById(int id) ;

     void Add(task newTask);
  
     void Update(task newTask);
     void Delete(int id);
}