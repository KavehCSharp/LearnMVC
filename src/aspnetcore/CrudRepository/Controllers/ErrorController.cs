using CrudRepository.Data;
using Microsoft.AspNetCore.Mvc;

namespace CrudRepository.Controllers
{
    public class ErrorController : Controller
    {
        protected ErrorService E;

        public ErrorController(ErrorService e)
        {
            E = e;
        }

        public IActionResult Index() => View(E.All);
    }
}