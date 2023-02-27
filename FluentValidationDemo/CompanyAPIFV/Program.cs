using CompanyAPIFV.Application;
using CompanyAPIFV.Application.Contracts;
using CompanyAPIFV.Application.Validators;
using CompanyAPIFV.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Services Registrations

builder.Services.AddValidatorsFromAssemblyContaining<RegisterEmployeeRequestValidator>();

builder.Services.AddTransient<EmployeeRepository>();
builder.Services.AddTransient<ProjectRepository>();

#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
