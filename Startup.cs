using AmiFlota.Data;
using AmiFlota.Models;
using AmiFlota.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AmiFlota
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

            services.AddDbContext<AmiFlotaContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AmiFlotaContext")));

            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<ITripService, TripService>();
            services.AddTransient<ICarService, CarService>();

            services.AddIdentity<ApplicationUserModel, IdentityRole>()
                .AddEntityFrameworkStores<AmiFlotaContext>()
                .AddDefaultTokenProviders();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            }
            );

            //Refresh pages without stopping application
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddHttpContextAccessor();

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
            app.UseSession();
/*            app.UseNodeModules();*/
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
