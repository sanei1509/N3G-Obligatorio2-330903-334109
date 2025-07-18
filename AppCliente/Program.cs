using System.Globalization;

namespace AppCliente
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // forzar el formato de fechas para evitar confusiones
            var defaultCulture = new CultureInfo("es-UY");
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

            var builder = WebApplication.CreateBuilder(args);

            // 1) Registrar MVC
            builder.Services.AddControllersWithViews();

            // 2) Registrar cach� en memoria para sesi�n
            builder.Services.AddDistributedMemoryCache();

            // 3) Registrar el servicio de sesi�n
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            // Add services to the container.
            builder.Services.AddControllersWithViews();

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
            
            // 6) **Insertar el middleware de sesi�n aqu�**
            app.UseSession();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Usuario}/{action=Login}");

            app.Run();
        }
    }
}
