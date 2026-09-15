using Microsoft.AspNetCore.Mvc;

namespace Fleet.API.Controllers
{
    public class FuelTransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
