using Dapper;
using DataApi.Models.Incidents;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataApi
{
    public class IncidentManager : IIncidentsManager
    {
        public string GetConnString(string connString)
        {
            return GlobalConfig.GetConnectionString(connString);
        }

        public void CreateIncident(Incident incidentRequest)
        {
            string connStr = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                var param = new DynamicParameters();

                param.Add("@Title", incidentRequest.Title);
                param.Add("@Description", incidentRequest.Description);
                param.Add("@Priority", incidentRequest.Priority);
                param.Add("@Category", incidentRequest.Category);
                param.Add("@Reporter", incidentRequest.Reporter);
                param.Add("@Impact", incidentRequest.Impact);
                param.Add("@Comments", incidentRequest.Comments);

                connection.Execute("dbo.CreateIncident", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Incident> GetAllIncidents()
        {
            string connStr = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                var incidents = connection.Query<Incident>("dbo.GetIncidents", commandType: System.Data.CommandType.StoredProcedure).ToList();

                return incidents;
            }
        }

        public Incident GetIncident(int incidentId)
        {
            string connStr = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                var incidents = connection.Query<Incident>("dbo.GetIncidents", commandType: System.Data.CommandType.StoredProcedure).ToList();

                return incidents.Find(item => item.Id == incidentId);
            }
        }

        public List<Incident> GetAssignedIncidents(string username)
        {
            string connStr = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                var param = new DynamicParameters();

                param.Add("@Username", username);

                return connection.Query<Incident>("dbo.GetAssigneeIncidents", param,
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void TakeOwnership(string username, int incidentId)
        {
            var connStr = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                var param = new DynamicParameters();

                param.Add("@Username", username);
                param.Add("@IncidentId", incidentId);

                connection.Execute("dbo.TakeOwnerShip", param, commandType: CommandType.StoredProcedure);
            }
        }

        public void Resolve(string resolution, int timeSpent, int incidentId)
        {
            var connStr = GetConnString("ContactsDB");

            using (SqlConnection connection = new SqlConnection(connStr))
            {
                var param = new DynamicParameters();

                param.Add("@Resolution", resolution);
                param.Add("@TimeSpent", timeSpent);
                param.Add("@IncidentId", incidentId);

                connection.Execute("dbo.Resolve", param, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
