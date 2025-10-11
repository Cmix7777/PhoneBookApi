public class Contact
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public virtual string GetDisplay()
    {
        return $"Контакт: {PhoneNumber} - {Name}";
    }
}