﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class tutorDBEntities : DbContext
    {
        public tutorDBEntities()
            : base("name=tutorDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Images> Images { get; set; }
        public virtual DbSet<LessonLevels> LessonLevels { get; set; }
        public virtual DbSet<LessonTypes> LessonTypes { get; set; }
        public virtual DbSet<RequestForAdminType> RequestForAdminType { get; set; }
        public virtual DbSet<Requests> Requests { get; set; }
        public virtual DbSet<RequestsForAdmins> RequestsForAdmins { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<SubjectTypes> SubjectTypes { get; set; }
        public virtual DbSet<Testimonials> Testimonials { get; set; }
        public virtual DbSet<TutorLessonLevel> TutorLessonLevel { get; set; }
        public virtual DbSet<TutorLessonType> TutorLessonType { get; set; }
        public virtual DbSet<TutorSubjects> TutorSubjects { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Tutors> Tutors { get; set; }
    }
}
