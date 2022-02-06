global using Newtonsoft.Json;
global using back.Models;
global using back.DialogueBD;
global using back.ModelsImport;
global using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// connection a la base de donn�e
builder.Services.AddDbContext<backlogContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("defaut")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    // genere un XML et permet de voir le sumary dans swagger pour chaque fonctions dans le controller
    string xmlNomFichier = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";
    swagger.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlNomFichier));
});

builder.Services.AddCors(options => options.AddPolicy("CORS", c => c.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // creer un JSON pour swagger
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "mon API V1");

        // cache les shemas des classes en bas de page
        c.DefaultModelsExpandDepth(-1);
    });
}

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();

// generer les models de la bdd
// Scaffold-DbContext "Data Source=desktop-j5htqcs\sqlserver;Initial Catalog=backlog;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models