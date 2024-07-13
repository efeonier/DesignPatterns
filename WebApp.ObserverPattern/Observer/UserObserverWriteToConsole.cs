using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApp.ObserverPattern.Entities;

namespace WebApp.ObserverPattern.Observer;

public class UserObserverWriteToConsole : IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public UserObserverWriteToConsole(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreated(AppUser appUser)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverWriteToConsole>>();
        logger.LogInformation("User Created. Id={AppUserId}", appUser.Id);
    }
}
