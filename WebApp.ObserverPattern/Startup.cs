using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.ObserverPattern.Context;
using WebApp.ObserverPattern.Entities;

namespace WebApp.ObserverPattern;

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
        services.AddDbContext<AppIdentityDbContext>(options =>
        {
            var connectionString = Configuration.GetConnectionString("SqlServer");
            options.UseSqlServer(connectionString);
        });

        services
            .AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>();

        // services.AddSingleton<UserObserverSubject>(sp =>
        // {
        //     var userObserverSubject = new UserObserverSubject();
        //     userObserverSubject.RegisterObserver(new UserObserverWriteToConsole(sp));
        //     userObserverSubject.RegisterObserver(new UserObserverCreateDiscount(sp));
        //     userObserverSubject.RegisterObserver(new UserObserverSendMail(sp));
        //
        //     return userObserverSubject;
        // });

        services.AddMediatR(Assembly.GetExecutingAssembly());

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
            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
