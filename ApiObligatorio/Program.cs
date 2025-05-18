using CasoUsoCompartida.DTOs.Envios;
using CasoUsoCompartida.DTOs.EtapaSeguimiento;
using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using Libreria.LogicaAplicacion.CasoUso.Usuarios;
using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosUso.Envios;
using LogicaAplicacion.CasosUso.Usuarios;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

//traduccion de enums
builder.Services
  .AddControllers()
  .AddJsonOptions(opts => {
      // Esto hará que todos los enums se serialicen como strings
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
builder.Services.AddScoped<IFinalizar, FinalizarEnvio>();
builder.Services.AddScoped<IGetByNroTracking<EnvioListadoDto>, GetByNroTracking>();


//Inyectar caso de uso de EtapaSeguimiento
builder.Services.AddScoped<IAdd<CrearComentarioDto>, AgregarComentario>();

//Inyectar el repositorio
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
builder.Services.AddScoped<IRepositorioEnvio, RepositorioEnvio>();
builder.Services.AddScoped<IRepositorioAgencia, RepositorioAgencia>();
builder.Services.AddScoped<IRepositorioEtapaSeguimiento, RepositorioEtapaSeguimiento>();


// inyectar el contexto
//builder.Services.AddDbContext<LibreriaContext>();
builder.Services.AddDbContext<LibreriaContext>(
     Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("ObligatorioDB"))
);


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
