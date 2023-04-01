
using DataApi.Models.Incidents;
using System.Collections.Generic;

namespace DataApi
{
    public interface IIncidentsManager
    {
        string GetConnString(string connString);

        void CreateIncident(Incident incidentRequest);

        List<Incident> GetAllIncidents();

        List<Incident> GetAssignedIncidents(string username);

        Incident GetIncident(int incidentId);

        void TakeOwnership(string username, int incidentId);

        void Resolve(string resolution, int timeSpent, int incidentId);
    }
}
