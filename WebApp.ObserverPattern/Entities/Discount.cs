using System.ComponentModel.DataAnnotations;

namespace WebApp.ObserverPattern.Entities;

public class Discount
{
    [Key] 
    public int Id { get; set; }
    public string UserId { get; set; }
    public int Rate { get; set; }
}