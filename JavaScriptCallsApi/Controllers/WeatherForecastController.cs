using System.Net;
using DataApi;
using JavaScriptCallsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JavaScriptCallsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get-weather-forecast")]
        private IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("post-weather-forecast")]
        private IEnumerable<WeatherForecast> Post()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToArray();
        }

        [HttpGet("titles")]
        private IEnumerable<string> GetTitles()
        {
            List<string> titles = new List<string>
            {
                "Mr",
                "Mrs",
                "Dr",
                "Miss",
                "Ms",
                "Prof"
            };

            return titles;
        }

        [HttpGet("relationships")]
        private IEnumerable<string> GetRelationships()
        {
            List<string> relationship = new List<string>
            {
                "Family",
                "Friend",
                "Colleague",
                "Partner"
            };

            return relationship;
        }

        [HttpPost("save-contact")]
        private ActionResult<string> SaveContact([FromBody]Contact contact)
        {
            try
            {
                //Save contact details to DB
                SqlConnector connector = new SqlConnector();

                ContactDetails details = new ContactDetails()
                {
                    Name = contact.Name,
                    LastName = contact.LastName,
                    CellNumber = contact.CellNumber,
                    Relationship = contact.Relationship
                };

                connector.CreateContact(details);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception exc)
            {
                throw;
            }
        }

        [HttpGet("get-contacts")]
        private ActionResult<IEnumerable<Contact>> ContactList()
        {

            SqlConnector connector = new SqlConnector();

            var contacts = connector.GetContacts();

            return MapContactList(contacts);
        }

        private List<Contact> MapContactList(List<ContactDetails> contactDetailsList)
        {
            List<Contact> contacts = new List<Contact>();

            foreach (var contact in contactDetailsList)
            {
                contacts.Add(new Contact()
                {
                    Name = contact.Name,
                    LastName = contact.LastName,
                    CellNumber = contact.CellNumber,
                    Relationship = contact.Relationship
                });
            }

            return contacts;
        }
    }
}