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
    
    public partial class homework
    {
        public homework()
        {
            this.uploadHomework = new HashSet<uploadHomework>();
        }
    
        public int id { get; set; }
        public int idMa { get; set; }
        public int idUs { get; set; }
        public string descriptions { get; set; }
        public string urldownload { get; set; }
        public string title { get; set; }
        public System.DateTime date_emision { get; set; }
        public System.DateTime date_end { get; set; }
    
        public virtual matter matter { get; set; }
        public virtual ICollection<uploadHomework> uploadHomework { get; set; }
    }
}
