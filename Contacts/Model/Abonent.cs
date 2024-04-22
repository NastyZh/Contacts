namespace Сontacts.Models;

public class Abonent
{
    public int Id { get; set; }
    public string FullName { get; set; }
    
    public Address Address{ get; set; }
    public PhoneNumber PhoneNumber { get; set; }
}