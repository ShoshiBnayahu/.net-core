 using ToDo.Interfaces; 
using ToDo.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ToDo.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddAuthentication(options =>
     {
         options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
     })
     .AddJwtBearer(cfg =>
     {
         cfg.RequireHttpsMetadata = true;
         cfg.TokenValidationParameters = TokenService.GetTokenValidationParameters();
     });
//


builder.Services.AddAuthorization(cfg =>
            {
                cfg.AddPolicy("Admin", policy => policy.RequireClaim("type", "Admin"));
                cfg.AddPolicy("User", policy => policy.RequireClaim("type", "User")); 
            });

            builder.Services.AddControllers();



builder.Services.AddSwaggerGen(c =>
   {
       c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo", Version = "v1" });
       //auth3
       c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
       {
           In = ParameterLocation.Header,
           Description = "Please enter JWT with Bearer into field",
           Name = "Authorization",
           Type = SecuritySchemeType.ApiKey
       });
       //auth4
       c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { new OpenApiSecurityScheme
                        {
                         Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                    new string[] {}
                }
       });

       
   });


builder.Services.AddSingleton<ITaskService,TaskService>();
builder.Services.AddSingleton<IUserService,UserService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//js
app.UseDefaultFiles();
app.UseStaticFiles();
//js


app.UselogMiddleware("file.log");

//Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseRouting();

//auth5
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();



app.Run();
