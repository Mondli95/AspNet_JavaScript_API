using DataApi;
using DataApi.Models.Incidents;
using JavaScriptCallsApi.Models.Incidents;
using Microsoft.AspNetCore.Mvc;

namespace JavaScriptCallsApi.Controllers
{
    [ApiController]
    [Route("incident-v1")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentsManager _incidentsManager;

        public IncidentController(IIncidentsManager incidentsManager) { _incidentsManager = incidentsManager; }

        [HttpPost("create-incident")]
        public ActionResult CreateIncident(CreateIncidentRequest createIncident)
        {
            var request = new Incident()
            {
                Category= createIncident.Category,
                Comments= createIncident.Comments,
                Description= createIncident.Description,
                Impact= createIncident.Impact,
                Priority= createIncident.Priority,
                Reporter= createIncident.Reporter,
                Title= createIncident.Title
            };

            _incidentsManager.CreateIncident(request);

            return Ok();
        }

        [HttpGet("get-incidents")]
        public ActionResult<IEnumerable<Incident>> GetAllIncidents()
        {
            return _incidentsManager.GetAllIncidents();
        }

        [HttpGet("incident-details")]
        public ActionResult<Incident> GetIncident([FromQuery] int id)
        {
            return _incidentsManager.GetIncident(id);
        }

        [HttpGet("assignee-incidents")]
        public ActionResult<List<Incident>> GetAssigneeIncidents([FromQuery] string username)
        {
            return _incidentsManager.GetAssignedIncidents(username);
        }

        [HttpPost("take-ownership")]
        public void TakeOwnership([FromBody] TakeOwnershipRequest ownershipRequest)
        {
            string username = ownershipRequest.Username;
            int incidentId = ownershipRequest.IncidentId;

            _incidentsManager.TakeOwnership(username, incidentId);
        }

        [HttpPost("resolve")]
        public void Resolve([FromBody] ResolveIncidentRequest resolveRequest)
        {
            string resolution = resolveRequest.Resolution;
            int time = resolveRequest.TimeSpent;
            int incidentId = resolveRequest.IncidentId;

            _incidentsManager.Resolve(resolution, time, incidentId);
        }
    }
}
