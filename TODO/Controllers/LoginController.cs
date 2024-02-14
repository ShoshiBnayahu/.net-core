namespace ToDo.Controllers;
using ToDo.Services;
using ToDo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class LoginController : ControllerBase
{
public LoginController(){

}

    [ApiController]
    [Route("[controller]")]
    public class loginController : ControllerBase
    {
      readonly UserService UserService;
       public loginController(UserService UserService) 
       { 
         this.UserService=UserService;
         }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<String> Login([FromBody] User User)
        {
          var getUser=UserService.GetAll()?.FirstOrDefault(u=>u.Name==User.Name&&u.Password==User.Password);
          var claims=new List<Claim>();
          if(getUser==null){
               if (User.Name != "Michal"|| User.Password != $"m123")
                      return Unauthorized();
                claims.Add(new Claim("type", "Admin"));
                ///כתיבה 
                UserService.Add(new User(){Id=User.Id,
                                            Name=User.Name,
                                            Password=User.Password});
          }
          else
              if(User.Name == "Michal" && User.Password == $"m123")
                      claims.Add(new Claim("type", "Admin"));
              else
                  claims.Add(new Claim("type", "User"));
          var token = ToDoTokenService.GetToken(claims);
           return new OkObjectResult(ToDoTokenService.WriteToken(token));
        }

          
           
            
    //     [HttpPost]
    //     [Route("[action]")]
    //     [Authorize(Policy = "Admin")]
    //     public IActionResult GenerateBadge([FromBody] Agent Agent)
    //     {
    //         var claims = new List<Claim>
    //         {
    //             new Claim("type", "Agent"),
    //             new Claim("UserName", Agent.Name),
    //             new Claim("ClearanceLevel", Agent.ClearanceLevel.ToString()),
    //         };

    //         var token = FbiTokenService.GetToken(claims);

    //         return new OkObjectResult(FbiTokenService.WriteToken(token));
    //     }
     }



}
