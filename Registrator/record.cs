//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Registrator
{
    using System;
    using System.Collections.Generic;
    
    public partial class record
    {
        public int id_record { get; set; }
        public int id_admin { get; set; }
        public string NameCom { get; set; }
        public System.DateTime dataCom { get; set; }
    
        public virtual adminDB adminDB { get; set; }
    }
}