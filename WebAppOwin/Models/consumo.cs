//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAppOwin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class consumo
    {
        public int id { get; set; }
        public int id_usuario { get; set; }
        public Nullable<System.DateTime> referencia { get; set; }
        public string ano_mes { get; set; }
        public Nullable<double> consumo_metros_cubicos { get; set; }
        public double leitura { get; set; }
        public Nullable<double> valor { get; set; }
    
        public virtual usuario usuario { get; set; }
    }
}
