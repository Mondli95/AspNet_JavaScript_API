using System;

namespace DataApi.Models.Incidents
{
    public class Incident
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Assignee { get; set; }
        public string Reporter { get; set; }
        public string Impact { get; set; }
        public string Resolution { get; set; }
        public int TimeSpent { get; set; }
        public string Comments { get; set; }
    }
}
