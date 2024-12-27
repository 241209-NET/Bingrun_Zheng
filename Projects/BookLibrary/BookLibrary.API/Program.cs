using BookLibrary.API.Data;
using BookLibrary.API.Repository;
using BookLibrary.API.Service;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookLibrary")));

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = 
    System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

    // //Dependency Inject the proper services
// builder.Services.AddScoped<IPetService, PetService>();
// builder.Services.AddScoped<IOwnerService, OwnerService>();

// //Dependency Inject the proper repositories
// builder.Services.AddScoped<IPetRepository, PetRepository>();
// builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
