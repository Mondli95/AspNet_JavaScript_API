using Microsoft.AspNetCore.Mvc;

namespace JavaScriptCallsApi.Controllers
{
    public class Blog : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
