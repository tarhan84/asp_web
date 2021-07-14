using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartSense.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSense
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer("Data Source=DESKTOP-T0CN7DN\\SQLEXPRESS;Initial Catalog=smartsense;Integrated Security=True;"));
            services.AddIdentity<User, IdentityRole>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

             services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false; 
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false; //@ * gibi karakterler olmalý

                options.Lockout.MaxFailedAccessAttempts = 5; //5 giriþten sonra kilitlenioyr. 
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(14); //5 dk sonra açýlýr
                options.Lockout.AllowedForNewUsers = true; //üsttekilerle alakalý

                //options.User.AllowedUserNameCharacters = ""; //olmasýný istediðiniz kesin karaterrleri yaz

                options.User.RequireUniqueEmail = false; //unique emaail adresleri olsun her kullanýcýnýn 

                options.SignIn.RequireConfirmedEmail = true; //Kayýt olduktan email ile token gönderecek 
                options.SignIn.RequireConfirmedPhoneNumber = false; //telefon doðrulamasý

                // Lockout                
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

            });



            services.ConfigureApplicationCookie(options => {
                options.LoginPath = "/Login/login";
                options.LogoutPath = "/Home/Index";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".SmartSense.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=login}/{id?}");

            });

        }

    }
}
