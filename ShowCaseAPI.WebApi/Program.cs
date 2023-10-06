using Amazon;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.IoC;
//using ShowCaseAPI.IoC;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Repository;
using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

//---------------------------------------------------------TODO: ADRI VOLTAR PARA POSTGRESQL --------------------------------------------------------------
// Obtenha as informações de conexão do appsettings.json
string server = builder.Configuration.GetConnectionString("Server");
string user = builder.Configuration.GetConnectionString("User");
string database = builder.Configuration.GetConnectionString("Database");

string password = Amazon.RDS.Util.RDSAuthTokenGenerator.GenerateAuthToken(RegionEndpoint.USEast1, server, 3306, user);
// Construa a ConnectionString para o MySQL
string connectionString = $"Server={server};Port=3306;Database={database};User ID={user};Password={password};SslMode=Required;SslCa=full_path_to_ssl_certificate";

// Configurar as opções do DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // Use o MySQL como provedor de banco de dados
    options.UseSqlServer(connectionString);
});
//---------------------------------------------------------TODO: ADRI VOLTAR PARA POSTGRESQL --------------------------------------------------------------

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