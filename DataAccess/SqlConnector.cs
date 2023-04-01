using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SqlConnector : IDataConnection
    {
        /// <summary>
        /// Save new contact to the database.
        /// </summary>
        /// <param name="contact">The contact information.</param>
        /// <returns>The newly created contact from the database</returns>
        public ContactDetails CreateContact(ContactDetails contact)
        {
            throw new NotImplementedException();
        }
    }
}
