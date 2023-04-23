using GeekShopping.web.Services;
using GeekShopping.web.Services.IServices;

namespace GeekShopping.web
{
    public class Program
    {   
        public static IConfiguration configuration { get; set; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            configuration = builder.Configuration;
            builder.Services.AddHttpClient<IProductService, ProductService>(c => c.BaseAddress = new Uri
            (configuration["ServicesUrls:ProductApi"]));
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}