using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyDemo.BLL.Interfaces;
using MyDemo.BLL.Repositories;
using MyDemo.DAL.Data.Contexts;
using MyDemo.PL.Mapping_Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDemo.PL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Allow Dependancy to MVC Project
            services.AddControllersWithViews();
            
            //Allow Dependancy to any Class Implements IDepartmentRepository
            //AddScoped means => Request åíÝÖá ÚÇíÔ Øæá ÝÊÑÉ Çá  object Çä Çá 
            services.AddScoped<IDepartmentRepository , DepartmentRepository>(); //Ask Clr If any one ask you for creating an object from any class implementing IDepartmentRepository 
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();                                                                    //Create it and inject it.

            services.AddDbContext<MVCDemoDbContext>(options => 
            
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(
                //name: "department",
                //pattern: "Department/{action=Index}/{id?}",
                //defaults: new { controller = "Department" });
            });
        }
    }
}
