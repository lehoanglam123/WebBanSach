using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;


namespace Webbansach.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QuanLiBanSachModel db = new QuanLiBanSachModel();
        public ActionResult Index()
        {
            return View(db.Saches.Where(n=>n.MaSach > 0).ToList());
        }
        
    }
}