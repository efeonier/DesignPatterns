using System.Collections.Generic;

namespace WebApp.CompositePattern.Entities;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public int RefId { get; set; }
    public ICollection<Book> Books { get; set; }
}
