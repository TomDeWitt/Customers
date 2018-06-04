namespace AscentCustomers.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calendar()
        {
            return View();
        }
        public ActionResult Employees()
        {
            return View();
        }

    }
}
