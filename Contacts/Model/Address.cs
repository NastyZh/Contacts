namespace Сontacts.Models;

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int NumberHouse { get; set; }

    public override string ToString()
    {
        return $"{Street} {NumberHouse}";
    }
}