using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace tareas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["id"] = 1;
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Upload() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult subir()
        {
            HttpFileCollectionBase files = Request.Files;
            if (files.Count > 0) 
            {
                
            }
            return View();
        }
        public string listar() 
        {
            string ruta = Server.MapPath("~/uploadhomeworks");
            string fullpaths="<ul>";
            DirectoryInfo p = new DirectoryInfo(ruta);
            foreach(var file in p.GetFiles())
            {
                fullpaths+="<li><b>"+file.Name+"</b></li>";
            }
            return ruta+"<br/> "+fullpaths+"</ul>";
        }
        public string prueba2() 
        {
            string ruta=Server.MapPath("~/uploadhomeworks");
            DirectoryInfo p = new DirectoryInfo(ruta);
            return "entra";
        }
        public string prueba() 
        {
            return "que pasa";
        }
    }
}
