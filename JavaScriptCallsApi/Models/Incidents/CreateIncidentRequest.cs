namespace JavaScriptCallsApi.Models.Incidents
{
    public class CreateIncidentRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public string Reporter { get; set; }
        public string Impact { get; set; }
        public string Comments { get; set; }
    }
}
