using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
//using ShowCaseAPI.IoC;

var builder = WebApplication.CreateBuilder(args);

// Obtenha as informações de conexão do appsettings.json
// Construa a ConnectionString para o PostgreeSQL
string connectionString = builder.Configuration.GetConnectionString("ShowCaseDB");

// Configurar as opções do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Use o MySQL como provedor de banco de dados
    options.UseNpgsql(connectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//NativeInjectorBootStrapper.RegisterServices(builder.Services);
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