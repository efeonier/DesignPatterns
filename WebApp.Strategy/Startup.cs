using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Strategy.Context;
using WebApp.Strategy.Entities;
using WebApp.Strategy.Enums;
using WebApp.Strategy.Models;
using WebApp.Strategy.Repository.Abstract;
using WebApp.Strategy.Repository.Concrete.MsSql;
using WebApp.Strategy.Services.Abstract;
using WebApp.Strategy.Services.Concrete;

namespace WebApp.Strategy
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("SqlServer");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<AppUser, IdentityRole>(options => { options.User.RequireUniqueEmail = true; })
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
          
            services.AddScoped<IProductRepository>(sp =>
            {
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var claim = httpContextAccessor.HttpContext?.User.Claims
                    .FirstOrDefault(w => w.Type == SettingsModel.ClaimDatabaseType);

                var dbContext = sp.GetRequiredService<AppIdentityDbContext>();
                if (claim == null) return new ProductRepository(dbContext);

                var databaseType = (EDatabaseType)int.Parse(claim.Value);

                return databaseType switch
                {
                    EDatabaseType.SqlServer => new ProductRepository(dbContext),
                    EDatabaseType.MySql => new Repository.Concrete.MySql.ProductRepository(dbContext),
                    EDatabaseType.MongoDb => new Repository.Concrete.MongoDb.ProductRepository(Configuration),
                    _ => new ProductRepository(dbContext)
                };
            });
        
            
            services.AddTransient<IProductService, ProductService>();
            services.AddControllersWithViews();
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
            });
        }
    }
}