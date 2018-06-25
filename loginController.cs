using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hotelproject.Controllers
{
    public class LoginController : Controller
    {
        Models.HotelDBEntities db = new Models.HotelDBEntities();

        // GET: login
        public ActionResult Index()
        {
            if (Session["user"] != null) Session["user"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Redirect(String email, String password)
        {
            var user = db.user.FirstOrDefault(i => (i.email == email && i.password == password));
            if(user != null)
            {
                db.Configuration.LazyLoadingEnabled = false;
                Session["user"] = user;
                return RedirectToAction("Index", "Reservation");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}