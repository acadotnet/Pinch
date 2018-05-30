using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pinch.Areas.Admin.Controllers
{

    [RoutePrefix("Recipes")]
    public class RecipesController : Controller
    {
        // GET: Admin/Recipes
        public ActionResult Index()
        {
            return View();
        }
    }
}