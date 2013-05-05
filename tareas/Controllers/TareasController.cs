using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tareas.Models;

namespace tareas.Controllers
{   
    public class TareasController : Controller
    {
        private tareasContext context = new tareasContext();

        //
        // GET: /Tareas/

        public ViewResult Index()
        {
            return View(context.Tareas.ToList());
        }

        //
        // GET: /Tareas/Details/5

        public ViewResult Details(int id)
        {
            Tarea tarea = context.Tareas.Single(x => x.id == id);
            return View(tarea);
        }

        //
        // GET: /Tareas/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Tareas/Create

        [HttpPost]
        public ActionResult Create(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                context.Tareas.Add(tarea);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(tarea);
        }
        
        //
        // GET: /Tareas/Edit/5
 
        public ActionResult Edit(int id)
        {
            Tarea tarea = context.Tareas.Single(x => x.id == id);
            return View(tarea);
        }

        //
        // POST: /Tareas/Edit/5

        [HttpPost]
        public ActionResult Edit(Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                context.Entry(tarea).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tarea);
        }

        //
        // GET: /Tareas/Delete/5
 
        public ActionResult Delete(int id)
        {
            Tarea tarea = context.Tareas.Single(x => x.id == id);
            return View(tarea);
        }

        //
        // POST: /Tareas/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Tarea tarea = context.Tareas.Single(x => x.id == id);
            context.Tareas.Remove(tarea);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}