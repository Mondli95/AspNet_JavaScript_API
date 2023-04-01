using System.Collections.Generic;

namespace DataApi
{
    public interface IDataConnection
    {
        void CreateContact(ContactDetails contact);
        List<ContactDetails> GetContacts();
    }
}
