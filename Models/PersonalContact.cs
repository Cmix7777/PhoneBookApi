public class PersonalContact : Contact
{
    public override string GetDisplay()
    {
        return $"Личный контакт: {PhoneNumber} - {Name}";
    }
}

