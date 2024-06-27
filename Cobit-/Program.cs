using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cobit_.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Cobit_Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cobit_Context") ?? throw new InvalidOperationException("Connection string 'Cobit_Context' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") //esto es para trabajar con nextjs, con las api
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
