using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcCore.Data;
using MvcCore.Repositories;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace MvcCore
{
    public class Startup
    {
        IConfiguration configuration { get; set; }

        public Startup(IConfiguration Configuration) { this.configuration = Configuration; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //DI / IOC Repositories
            services.AddTransient<RepositoryCochesSql>();
            services.AddTransient<RepositoryCochesMySql>();
            services.AddTransient<IRepositoryCoches,RepositoryCochesSql>();
            services.AddTransient<IRepositoryCoches,RepositoryCochesMySql>();
            //Services of DI & IOC bbdd
            //String cadena = this.configuration.GetConnectionString("Sql");
            String cadena = this.configuration.GetConnectionString("MySql");
            services.AddDbContext<CochesContext>(options => options.UseMySql(cadena, new MySqlServerVersion(new Version(8, 0, 22)),
              MySQLOptionsAction => MySQLOptionsAction.CharSetBehavior(CharSetBehavior.NeverAppend)));
            //services.AddDbContext<CochesContext>(options => options.UseSqlServer(cadena));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
