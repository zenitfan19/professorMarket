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
    
    public partial class Requests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Requests()
        {
            this.Testimonials = new HashSet<Testimonials>();
        }
    
        public long id { get; set; }
        public long tutorId { get; set; }
        public long studentId { get; set; }
        public long subjectId { get; set; }
        public long lessonsTypeId { get; set; }
        public System.DateTime date { get; set; }
        public string additionalInfo { get; set; }
        public string status { get; set; }
    
        public virtual Subjects Subjects { get; set; }
        public virtual TutorLessonType TutorLessonType { get; set; }
        public virtual Users Tutors { get; set; }
        public virtual Users Students { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Testimonials> Testimonials { get; set; }
        public virtual LessonTypes LessonTypes { get; set; }
    }
}
