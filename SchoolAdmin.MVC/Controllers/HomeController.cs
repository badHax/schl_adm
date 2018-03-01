using System.Web.Mvc;

namespace SchoolAdmin.MVC.Controllers
{
    [RoutePrefix("Home/")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //Get
        [Route("Dashboard")]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}