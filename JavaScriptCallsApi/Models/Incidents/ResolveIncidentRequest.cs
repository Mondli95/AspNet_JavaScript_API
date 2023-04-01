namespace JavaScriptCallsApi.Models.Incidents
{
    public class ResolveIncidentRequest
    {
        public string Resolution { get; set; }
        public int TimeSpent { get; set; }
        public int IncidentId { get; set; }
    }
}
