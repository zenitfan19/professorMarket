//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tutors
    {
        public long userId { get; set; }
        public Nullable<long> avatarId { get; set; }
        public string Skype { get; set; }
        public string info { get; set; }
        public string adress { get; set; }
        public string education { get; set; }
        public string qualification { get; set; }
        public Nullable<bool> isApproved { get; set; }
        public Nullable<double> rating { get; set; }
    
        public virtual Images Images { get; set; }
        public virtual Users Users { get; set; }
    }
}
