using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Cors;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.IoC;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Repository;
using System.Text;
using ShowCaseAPI.WebApi.Helper;
using ShowCaseAPI.WebApi.Model.Blob;

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

//NATI AQUI!
//Token
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API Name", Version = "v1" });

    // Configura��o para incluir o JWT no Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Data - Repository
NativeInjectorBootStrapper.RegisterServices(builder.Services);

//Configure BLOB
var blobSection = builder.Configuration.GetSection("AccessBlob");
builder.Services.Configure<BLOBSetup>(blobSection);
var blobSetup = blobSection.Get<BLOBSetup>();
BlobInstance.AccessKey = blobSetup.AccessKey;
BlobInstance.AccessName = blobSetup.AccessName;

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

// Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();