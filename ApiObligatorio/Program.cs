using ApiObligatorio.Services;
using CasoUsoCompartida.DTOs;
using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.EtapaSeguimiento;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.DTOS.Envios;
using CasoUsoCompartida.InterfacesCU;
using Libreria.LogicaAplicacion.CasoUso.Usuarios;
using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosUso.Agencias;
using LogicaAplicacion.CasosUso.Envios;
using LogicaAplicacion.CasosUso.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

//traduccion de enums
builder.Services
  .AddControllers()
  .AddJsonOptions(opts => {
      // Esto har� que todos los enums se serialicen como strings
      opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
  });


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inyectar caso de uso de Usuario
builder.Services.AddScoped<IAdd<CrearUsuarioDto>, CrearUsuario>();
builder.Services.AddScoped<IRemove, BorrarUsuario>();
builder.Services.AddScoped<IUpdate<CrearUsuarioDto>, EditarUsuario>();
builder.Services.AddScoped<IGetByEmail<UsuarioDto>, GetByEmail>();
builder.Services.AddScoped<IGetById<UsuarioDto>, GetById>();
builder.Services.AddScoped<IGetById<CrearUsuarioDto>, GetByIdEditar>();
builder.Services.AddScoped<IGetAll<UsuarioListadoDto>, GetAllUsuario>();
builder.Services.AddScoped<ILogin<LoginRespuestaDto>, Login>();

//Inyectar caso de uso de Envio
builder.Services.AddScoped<IAdd<CrearEnvioDto>, CrearEnvio>();
builder.Services.AddScoped<IGetAll<EnvioListadoDto>, GetAllEnvios>();
builder.Services.AddScoped<IGetAll<AgenciaListadoDto>, GetAllAgencias>();
builder.Services.AddScoped<IFinalizar, FinalizarEnvio>();
builder.Services.AddScoped<IGetByNroTracking<EnvioListadoDto>, GetByNroTracking>();

//Inyectar caso de uso de EtapaSeguimiento
builder.Services.AddScoped<IAdd<CrearComentarioDto>, AgregarComentario>();

//Inyectar el repositorio
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioEnvio, RepositorioEnvio>();
builder.Services.AddScoped<IRepositorioAgencia, RepositorioAgencia>();
builder.Services.AddScoped<IRepositorioEtapaSeguimiento, RepositorioEtapaSeguimiento>();
builder.Services.AddScoped<IRepositorioAuditoria, RepositorioAuditoria>();



// inyectar el contexto
//builder.Services.AddDbContext<LibreriaContext>();
builder.Services.AddDbContext<LibreriaContext>(
     Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("ObligatorioDB"))
);


var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();


//AGREGAR JWT
var jwtConfig = builder.Configuration.GetSection("JWT");
var key = Encoding.ASCII.GetBytes(jwtConfig["Key"]);
builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();

builder.Services.AddAuthentication(
    options => 
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false; // En producción: true
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // sin tolerancia extra de expiración
        };
    });
    builder.Services.AddAuthorization();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();   // ?? Esto es esencial para que funcione en produccion y en todos lados
app.UseAuthorization();

app.MapControllers();

app.Run();
