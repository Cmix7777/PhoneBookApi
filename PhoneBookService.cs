using System.Collections.Generic;
using PhoneA.Models;

namespace PhoneA
{
    public class PhoneBookService
    {
        private static Dictionary<string, Contact> _contacts = new Dictionary<string, Contact>();

        public static Dictionary<string, Contact> Contacts => _contacts;

        static PhoneBookService()
        {
            _contacts.Add("1", new PersonalContact { Id = "1", Name = "Иван Иванов", PhoneNumber = "+71234567890" });
            _contacts.Add("2", new BusinessContact { Id = "2", Name = "ООО", PhoneNumber = "+70987654321" });
            
        }
    }
}