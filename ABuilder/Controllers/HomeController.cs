using ABuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
namespace ABuilder.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult Equation(int EquationId)
        {
            dynamic result = 0;
            using (var container = new ABuilderContainer())
            {
                var eq = container.Equations.Find(EquationId);
                if (eq != null)
                    result = new NCalc.Expression(eq.Value).Evaluate();
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}
