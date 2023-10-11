using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Database;
using System.Text.Json.Serialization;

namespace net_il_mio_fotoalbum
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // CODICE RIMOSSO COME DA ISTRUZIONI SLIDE PER AUTHENTICATION
            //var connectionString = builder.Configuration.GetConnectionString("ImageContextConnection") ?? throw new InvalidOperationException("Connection string 'ImageContextConnection' not found.");
            
            // PRECEDENTEMENTE DA LEVARE, per test funzionamento, invece, la modifico
            builder.Services.AddDbContext<ImageContext>();
            // contenuto di ImageContext 'options =>options.UseSqlServer(connectionString)'

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<ImageContext>();
           // TEST stringa da comparare
          //  builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<PizzaContext>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Codice aggiunto per evitare che la chiamata Api, vada il loop
            // Codice di configurazione per il serializzatore Json
            // in modo che ignori completamente le dipendenze cicliche di eventuali relazioni N:N o 1:N
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            //DEPENDECY INJECTION
            builder.Services.AddScoped<ImageContext, ImageContext>();

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

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}