using WebApp.ObserverPattern.Entities;

namespace WebApp.ObserverPattern.Observer;

public interface IUserObserver
{
    void UserCreated(AppUser appUser);
}