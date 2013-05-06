using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using tareas.Models.ModelViews;
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
        public IEnumerable<TareasView> loadMainPage()
        {
            IEnumerable<TareasView> lista = contexto.matter.Select(a => new TareasView() { code = a.code, name=a.name,tareas=a.homework});
            return lista;
        }
    }
}