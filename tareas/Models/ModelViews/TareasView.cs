using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tareas.Models.ModelViews
{
    public class TareasView:matter
    {
        public IEnumerable<homework> tareas { set; get;}
    }
}