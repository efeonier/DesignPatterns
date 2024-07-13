using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApp.ObserverPattern.Events;

namespace WebApp.ObserverPattern.EventHandlers;

public class CreatedUserWriteToConsoleEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<CreatedUserWriteToConsoleEventHandler> _logger;

    public CreatedUserWriteToConsoleEventHandler(ILogger<CreatedUserWriteToConsoleEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User Created. Id={AppUserId}", notification.AppUser.Id);
        return Task.CompletedTask;
    }
}
