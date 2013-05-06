using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tareas.Models.ModelViews;
using System.Web.Mvc;
using System.Web.Security;
using tareas.Controllers;
namespace tareas.Models
{
    public class HomeWorkContext
    {
        #region adminquery
        public HomeEntities contexto = new HomeEntities();
        public HomeWorkContext()
        {

        }
        public bool save(matter materia) 
        {
           
            try 
            {
                contexto.matter.Add(materia);
                contexto.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
            
        }
        public bool update(MateriasView materias) 
        {
            try
            {
                matter mat = getMateria(materias.id);
                mat.name = materias.name;
                mat.code = materias.code;
                contexto.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }
        public matter getMateria(int id) 
        {
            return contexto.matter.ToList().Where(a=>a.id==id).First();
        }
        public List<matter> getMaterias() 
        {
            return contexto.matter.ToList();
        }

        internal bool delete(int id)
        {
            try
            {
                contexto.matter.Remove(getMateria(id));
                contexto.SaveChanges();
                return true;
            }
            catch {
                return false;
            }

        }
        #endregion
        //carga la página principal
        public List<TareasView> loadMainPage(int id)
        {
            IEnumerable<TareasView> lista = contexto.matter.Select(a => new TareasView()
            {
                code = a.code,
                name = a.name,
                tareas = a.homework.Select(b => new HomeWorkView() {idUs=b.idUs,title=b.title,descriptions=b.descriptions,urldownload=b.urldownload})
            });
            UsersContext user = new UsersContext();
            List<TareasView> data = lista.ToList();
            foreach (TareasView item in data)
            {
                //item.name="HOLAAA";
                foreach(HomeWorkView work in item.tareas.ToList())
                {
                    work.add = true;
                    IEnumerable<webpages_Roles> roles=contexto.webpages_UsersInRoles.Where(a => a.UserId == id).Select(a => a.webpages_Roles);
                    if (roles.Count() > 0) 
                    {
                        if(work.idUs==id)
                        {
                            foreach(var us in roles)
                            {
                                switch(us.RoleName)
                                {
                                    case "AgregarTarea": 
                                        {
                                           work.add = true;
                                            break;
                                        }
                                    case "EditarTarea": 
                                        {
                                            work.edit = true;
                                            break;
                                        }
                                    case "BorrarTarea": 
                                        {
                                            work.delete = true;
                                            break;
                                        }
                                }
                            }
                        }
                    } 
              }
            }
            //dynamic dd = arreglo;
            return data;
        }
    }
}