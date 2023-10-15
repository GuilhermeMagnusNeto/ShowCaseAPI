using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.IoC;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Repository;

var builder = WebApplication.CreateBuilder(args);

// Obtenha as informa��es de conex�o do appsettings.json
// Construa a ConnectionString para o PostgreeSQL
string connectionString = builder.Configuration.GetConnectionString("ShowCaseDB");

// Configurar as op��es do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

// Data - Repository
NativeInjectorBootStrapper.RegisterServices(builder.Services);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

// Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();