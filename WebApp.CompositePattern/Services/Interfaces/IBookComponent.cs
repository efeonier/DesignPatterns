namespace WebApp.CompositePattern.Services.Interfaces;

public interface IBookComponent
{
    public int Id { get; set; }
    public string Name { get; set; }
    int Count();
}
