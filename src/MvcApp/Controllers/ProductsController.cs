using Microsoft.AspNetCore.Mvc;
using Randstad.Solutions.AspNetCoreRouting.Attributes;

namespace MvcApp.Controllers
{
    [Translate("fr", "produits")]
    public class ProductsController : Controller
    {
        public IActionResult Index(string id)
        {
            return View();
        }
        
        public IActionResult Detail(string id)
        {
            return View();
        }
    }
}