using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Intelogix.TimeTracker.Core.Services.AuthService;
using Intelogix.TimeTracker.Core.Services.EmployeeService;
using Intelogix.TimeTracker.Data;
using Intelogix.TimeTracker.Repository.UnitOfWork;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddDbContext<TimeTrackerDbContext>(options => options
            .UseSqlServer(builder.Configuration["ConnectionString"])
            );
builder.Services.AddScoped<ITimeTrackerUnitOfWork,TimeTrackerUnitOfWork>();
builder.Services.AddScoped<IAuthServiceManager,AuthServiceManager>();
builder.Services.AddScoped<IEmployeeServiceManager,EmployeeServiceManager>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Default",
                          policy =>
                          {
                              policy.
                              AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                          });
});
var app = builder.Build();
app.UseCors("Default");
//Migrate db
using (var scope =
  app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<TimeTrackerDbContext>())
    context.Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
