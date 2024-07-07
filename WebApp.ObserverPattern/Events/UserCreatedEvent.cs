using MediatR;
using WebApp.ObserverPattern.Entities;

namespace WebApp.ObserverPattern.Events;

public class UserCreatedEvent : INotification
{
    public AppUser AppUser { get; set; }
}