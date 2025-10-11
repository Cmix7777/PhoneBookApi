namespace PhoneA.Models
{
    public class CreateContactDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; } = "Personal";
    }
}