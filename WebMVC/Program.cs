using CasoUsoCompartida.DTOs.Usuarios;
using CasoUsoCompartida.InterfacesCU;
using LogicaAccesoDatos.EF;
using LogicaAplicacion.CasosUso.Usuarios;
using LogicaNegocio.InterfacesRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

//Inyectar caso de uso de Usuario
builder.Services.AddScoped<IAdd<CrearUsuarioDto>, CrearUsuario>();
builder.Services.AddScoped<IGetByEmail<UsuarioDto>, GetByEmail>();
builder.Services.AddScoped<ILogin<LoginRespuestaDto>, Login>();

//Inyectar el repositorio
builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();

//Inyectar el contexto
builder.Services.AddDbContext<LibreriaContext>();

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
