using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp.ObserverPattern.Context;
using WebApp.ObserverPattern.Entities;

namespace WebApp.ObserverPattern.Observer;

public class UserObserverCreateDiscount : IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public UserObserverCreateDiscount(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreated(AppUser appUser)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverCreateDiscount>>();

        using (var scoped = _serviceProvider.CreateScope())
        {
            var context = scoped.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
            context.Discounts.Add(new Discount { UserId = appUser.Id, Rate = 10 });
            context.SaveChanges();
        }

        logger.LogInformation("User Created. Add Discount");
    }
}
