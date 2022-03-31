using System.Web.Mvc;

namespace WebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Json(new { Version = 1.0, AppName = "WebApi" }, JsonRequestBehavior.AllowGet);
        }
    }
}