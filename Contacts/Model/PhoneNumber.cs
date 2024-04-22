namespace Сontacts.Models;

public class PhoneNumber
{
    public int Id { get; set; }
    public string Number { get; set; }
    public PhoneType Type { get; set; }
    public override string ToString()
    {
        return $"{Number}   {Type}";
    }
}