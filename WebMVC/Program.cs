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
using LogicaNegocio.Entidades.Envios;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession();

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

//Inyectar el contexto
builder.Services.AddDbContext<LibreriaContext>();

//Registro HTTP para obtener info de la session
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Usuario}/{action=Login}/{id?}");

app.Run();
