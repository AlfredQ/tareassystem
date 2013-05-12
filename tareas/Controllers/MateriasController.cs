using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tareas.Models;
using tareas.Models.ModelViews;
namespace tareas.Controllers
{
    public class MateriasController : Controller
    {
        #region admin
        private HomeWorkContext db = new HomeWorkContext();
        public ActionResult Index()
        {

            MateriasView materias = new MateriasView();
            materias.materias = db.getMaterias();

            return View(materias);
        }
        [HttpPost]
        public ActionResult Index(MateriasView materia)
        {

            matter mat = new matter()
            {
                code = materia.code,
                date_register = DateTime.Now,
                name = materia.name,
            };
            db.save(mat);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Edit(int id)
        {

            matter materia = db.getMateria(id);
            MateriasView vistaMaterias = new MateriasView()
            {
                id = materia.id,
                name = materia.name,
                code = materia.code
            };
            vistaMaterias.materias = db.getMaterias();
            return View(vistaMaterias);
        }


        [HttpPost]
        public ActionResult Edit(MateriasView materias)
        {
            try
            {
                db.update(materias);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            db.delete(id);
            return RedirectToAction("index");
        }
        #endregion
        public ViewResult Lista()
        {
            List<TareasView> model;
            if (Session["id"] != null)
            {
                model = db.loadMainPage((int)Session["id"]);

            }
            else
            {
                model = db.loadMainPage(0);

            }
            IEnumerable<TareasView> m = (IEnumerable<TareasView>)model;
            return View(m);
        }
        public ViewResult AgregarTarea(string id)
        {
            ViewBag.idma = id;
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult AgregarTarea(homework home, string filenames) 
        {
            if (Session["id"] != null)
                home.idUs = (int)Session["id"];
            if (db.insertHomeworks(home,filenames))
            {
                return RedirectToAction("Lista");
            }
            else {
                return RedirectToAction("AgregarTarea");
                
            }
        }
        public ViewResult upload()
        {

            return View();
        }
        /*
         Subir archivos
         */
        [HttpPost]
        public JsonResult SaveFiles()
        {
            HttpFileCollectionBase files = Request.Files;
            string path = Server.MapPath("~/uploadhomeworks/");
            string  nombres_de_arcnivos = "";
            string respuesta = "";
            for (int i = 0; i < files.Count; i++)
            {
                if (files[i].ContentLength > 0)
                {
                    string[] archivoenpartes = files[i].FileName.Split('.');
                    if (archivoenpartes.Length == 0)
                    {
                        respuesta = "false";
                        return Json(data: respuesta);
                    }
                    nombres_de_arcnivos += files[i].FileName + "|";
                    string extension = archivoenpartes[1];
                    string nombredearchivo = archivoenpartes[0];
                    string name = DateTime.Now.GetHashCode().ToString();
                    files[i].SaveAs(path + name+ "." + extension);
                    respuesta += "/uploadhomeworks/" + name + "." + extension+"|";
                }
                else
                {
                    respuesta = "false";
                }
            }

            return Json(data: new ResultUpload() {filename=nombres_de_arcnivos,fileroute=respuesta });
        }
        public JsonResult borrar_archivos(int idHome,int idFile) 
        {
            bool resultadodeborradodearchivos = db.deleteFilesHomeWork(idFile);

            return Json(data: resultadodeborradodearchivos);
        }
        /*
         Edicion de archivos
         */
        public ViewResult actualizarTarea(int id) 
        {
            homework tarea=db.getTareas(id);

            return View(tarea);
        }
        [HttpPost]
        public RedirectToRouteResult actualizarTarea(homework home, string filenames) 
        {
            db.updateTarea(home,filenames);
            return RedirectToAction("Lista");
        }
        public JsonResult borrarTarea(int id) 
        {
            bool result = false;
            result=db.deleteAllHomework(id);
            return Json(data:result);
        }
    }
}
