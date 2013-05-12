using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tareas.Models.ModelViews
{
    public class TareasView:matter
    {
        public IEnumerable<HomeWorkView> tareas { set; get;}
        public bool add { set; get; }
        
    }
}