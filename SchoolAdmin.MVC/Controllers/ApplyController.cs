using System.Web.Mvc;

namespace SchoolAdmin.MVC.Controllers
{
    [Authorize(Roles = "student")]
    [Authorize(Roles = "roles")]
    public class ApplyController : Controller
    {
        // GET: Apply
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}