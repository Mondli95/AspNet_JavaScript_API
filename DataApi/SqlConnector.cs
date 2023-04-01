using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DataApi
{
    public class SqlConnector : IDataConnection
    {
        /// <summary>
        /// Save new contact to the database.
        /// </summary>
        /// <param name="contact">The contact information.</param>
        /// <returns>The newly created contact from the database</returns>
        public void CreateContact(ContactDetails contact)
        {
            var connectionStr = GlobalConfig.GetConnectionString("ContactsDB");

            using (IDbConnection connection = new SqlConnection(connectionStr))
            {
                var param = new DynamicParameters();
                param.Add("@Name", contact.Name);
                param.Add("@LastName", contact.LastName);
                param.Add("@CellNumber", contact.CellNumber);
                param.Add("@Relationship", contact.Relationship);

                connection.Execute("dbo.CreateContact", param, commandType:CommandType.StoredProcedure);
            }
        }

        public List<ContactDetails> GetContacts()
        {
            var connectionStr = GlobalConfig.GetConnectionString("ContactsDB");

            List<ContactDetails> contactsList;

            using (IDbConnection connection = new SqlConnection(connectionStr))
            {
                contactsList = connection.Query<ContactDetails>("dbo.GetContacts", null, commandType: CommandType.StoredProcedure).ToList();
            }

            return contactsList;
        }
    }
}
