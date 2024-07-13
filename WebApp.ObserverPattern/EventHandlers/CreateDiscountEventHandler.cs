using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using WebApp.ObserverPattern.Context;
using WebApp.ObserverPattern.Entities;
using WebApp.ObserverPattern.Events;

namespace WebApp.ObserverPattern.EventHandlers;

public class CreateDiscountEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly AppIdentityDbContext _context;
    private readonly ILogger<CreateDiscountEventHandler> _logger;

    public CreateDiscountEventHandler(AppIdentityDbContext context, ILogger<CreateDiscountEventHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        await _context.Discounts.AddAsync(new Discount { UserId = notification.AppUser.Id, Rate = 10 }, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("User Created. Add Discount");
    }
}
