using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeerlessInterview.src.Api.Exception.Handler;
using PeerlessInterview.src.Data;
using PeerlessInterview.src.Repository.Customer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("myDb");
});

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<ICustomerRepository, ICustomerRepositoryImpl>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();


app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
