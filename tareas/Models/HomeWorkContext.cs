﻿using System;
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
        public Entities contexto = new Entities();
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
                id=a.id,
                tareas = a.homework.Select(b => new HomeWorkView() {idUs=b.idUs,title=b.title,descriptions=b.descriptions,urldownload=b.urldownload,date_emision=b.date_emision,date_end=b.date_end,id=b.id})
            });
            UsersContext user = new UsersContext();
            List<TareasView> data = lista.ToList();
            foreach (TareasView item in data)
            {
                //item.name="HOLAAA";
                item.add = agregarTareaVerificacion(id,item.id);
                
                foreach(HomeWorkView work in item.tareas.ToList())
                {
                    IEnumerable<webpages_Roles> roles=contexto.webpages_UsersInRoles.Where(a => a.UserId == id).Select(a => a.webpages_Roles);
                    if (roles.Count() > 0) 
                    {
                        if(work.idUs==id)
                        {
                            foreach(var us in roles)
                            {
                                switch(us.RoleName)
                                {
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

        private bool agregarTareaVerificacion(int id,int idMa)
        {
            //en cado de contar con el permiso asignado
            //desde la tabla
            if(contexto.usuario_materias.Where(a=>a.idUs==id&&a.idMa==idMa).Count()>0)
            {
                return true;
            }
            /*
             En caso de ser administrador del sistema
             */
            if (contexto.webpages_UsersInRoles.Where(a => a.UserId == id && a.webpages_Roles.RoleName == "AgregarTarea").Count() > 0) 
            {
                return true;
            }
            return false;
        }

        internal bool insertHomeworks(homework home, string filenames)
        {
            /*
             Insertamos la talbla correspondiente a los archivos de cada tarea emitida
             * sobre la tabla fileHomework
             */
            string dataurl = home.urldownload;
                home.urldownload = "none";
                contexto.homework.Add(home);
                contexto.SaveChanges();

                addWorksFiles(dataurl,home.id,filenames);
                return true;
        }
        internal bool addWorksFiles(string url,int idHome,string filenames) 
        {
            string[] routefiles = url.Split('|');
            string[] file = filenames.Split('|');
            int i;
            for (i = 0; i < routefiles.Length - 1; i++)
            {
                filesHomework files = new filesHomework()
                {
                    idHome = idHome,
                    url = routefiles[i],
                    descripcion = file[i]
                };

                contexto.filesHomework.Add(files);
                
            }
            contexto.SaveChanges();
            return true;
        }
        internal homework getTareas(int id)
        {

            //IEnumerable<filesHomework> result=contexto.filesHomework.Where(a => a.idHome == id);
            homework tarea = contexto.homework.Where(a => a.id == id).First();
            return tarea;
        }
        internal bool deleteAllFilesHomework(int id) 
        {
            IEnumerable<filesHomework> lista=contexto.filesHomework.Where(a => a.idHome == id);
            foreach(filesHomework item in lista)
            {
                contexto.filesHomework.Remove(item);
            }
            try
            {
                contexto.SaveChanges();
                return true;

            }
            catch {
                return false;
            }
        }
        internal void updateTarea(homework home,string filesnames)
        {
            homework tarea = getTareas(home.id);
            tarea.title = home.title;
            tarea.date_emision = home.date_emision;
            tarea.date_end = home.date_end;
            tarea.descriptions = home.descriptions;
            contexto.SaveChanges();
            if (home.urldownload != "none") 
            {
                //deleteFilesHomework(home.id);
                addWorksFiles(home.urldownload,home.id,filesnames);
            }
        }

        internal bool deleteHomeWork(int idHome)
        {
            homework home= contexto.homework.Where(a => a.id == idHome).First();
            contexto.homework.Remove(home);
            try
            {

                contexto.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        internal bool deleteFilesHomeWork(int idFile)
        {
           filesHomework archivos=contexto.filesHomework.Where(a => a.id == idFile).First();
           contexto.filesHomework.Remove(archivos);
           try
           {
               contexto.SaveChanges();
               return true; 
           }
           catch {
               return false;
           }
            
        }

        internal bool deleteAllHomework(int id)
        {
            if (deleteAllFilesHomework(id)) {
                return deleteHomeWork(id);
            };
            return false;
        }
    }
}