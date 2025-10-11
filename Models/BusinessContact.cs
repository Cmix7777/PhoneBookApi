namespace PhoneA.Models
{
    public class BusinessContact : Contact
    {
        public override string GetDisplay()
        {
            return $"Бизнес контакт: {PhoneNumber} - {Name}";
        }
    }
}