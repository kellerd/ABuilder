//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ABuilder.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SingleModel
    {
        public SingleModel()
        {
            this.Stats = new HashSet<SingleModel_Stat>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<SingleModel_Stat> Stats { get; set; }
    }
}
