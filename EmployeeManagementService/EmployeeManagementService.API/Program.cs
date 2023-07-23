using EmployeeManagementService.API.Mapping;
using EmployeeManagementService.BL.Services;
using EmployeeManagementService.Contracts.Abstraction.Repositories;
using EmployeeManagementService.Contracts.Abstraction.Services;
using EmployeeManagementService.DAL.Data;
using EmployeeManagementService.DAL.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddSingleton<DataContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var config = new TypeAdapterConfig();
builder.Services.AddSingleton<IMapper, ServiceMapper>();

var mapping = new Mapping();
mapping.Register(config);
builder.Services.AddSingleton(config);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    await context.Init();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
