//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tareas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class rateHomeWork
    {
        public int id { get; set; }
        public double rate { get; set; }
        public string observation { get; set; }
        public int idUs { get; set; }
        public int idUp { get; set; }
    
        public virtual uploadHomework uploadHomework { get; set; }
    }
}
