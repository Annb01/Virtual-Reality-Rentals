using Microsoft.EntityFrameworkCore;
using VR_Rentals.Data;
using VR_Rentals.Mapping;
using VR_Rentals.Models;
using VR_Rentals.Repositories;
using VR_Rentals.Services;
using Microsoft.AspNetCore.Identity;

namespace VR_Rentals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<RentalContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<Customer>(options => options.SignIn.RequireConfirmedAccount = true) .AddEntityFrameworkStores<RentalContext>();
            builder.Services.AddIdentity<Customer, IdentityRole<int>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<RentalContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IRentalRepository, RentalRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IRentalService, RentalService>();
            builder.Services.AddScoped<IVrEquipmentRepository, VrEquipmentRepository>();
            builder.Services.AddRazorPages();

            builder.Services.AddSession();
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            var app = builder.Build();

            app.UseSession();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<RentalContext>();
                context.Database.Migrate();
                DbInitializer.SeedData(context);
            }
            app.MapRazorPages();

            app.Run();
        }
    }
}
