using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tareas.Models.ModelViews
{
    public class HomeWorkView:homework
    {
        public bool add { set; get; }
        public bool edit { set; get; }
        public bool delete { set; get; }
    }
}