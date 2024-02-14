using ToDo.Models;
using System.Collections.Generic;


namespace ToDo.Interfaces;

public  interface IUserService
{

    List<User> GetAll() ;

     User GetById(int id) ;

     void Add(User newUser);
  
     void Update(User newUser);
     void Delete(int id);
}