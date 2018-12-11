using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data
{
    public static class StudentData
    {
        public static StudentDTO GetStudent(long? id = null, string email = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(email))
                return null;
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbStudent = ctx.Users.Join(ctx.Students,
                        u => u.id,
                        s => s.userId,
                        (u, s) => new
                        {
                            id = u.id,
                            name = u.name,
                            email = u.email,
                            emailVerified = u.emailVerified,
                            passwordHash = u.passwordHash,
                            passwordSalt = u.passwordSalt,
                            avatarId = s.avatarId,
                            Skype = s.Skype,
                            info = s.info,
                            adress = s.adress,
                            lessonsLevelId = s.lessonsLevelId,
                            birthDate = u.birthDate,
                            regDate = u.regDate

                        }).FirstOrDefault(x => x.id == id || x.email == email);
                    if (dbStudent != null)
                    {
                        var student = new StudentDTO
                        {
                            id = dbStudent.id,
                            name = dbStudent.name,
                            email = dbStudent.email,
                            emailVerified = dbStudent.emailVerified,
                            avatarId = dbStudent.avatarId,
                            Skype = dbStudent.Skype,
                            info = dbStudent.info,
                            adress = dbStudent.adress,
                            lessonsLevelId = dbStudent.lessonsLevelId,
                            birthDate = dbStudent.birthDate,
                            regDate = dbStudent.regDate

                        };

                        return student;
                    }

                    throw new Exception($"Пользователь с email: {email} не найден");
                }
            }
            catch (Exception ex)
            {
                //throw;
                return null;
            }

        }

        public static long UpdateStudent(StudentDTO student)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.id == student.id) ?? throw new Exception($"Пользователя не существует");                   


                    dbUser.name = student.name;
                    dbUser.email = student.email;                    
                    //dbUser.emailVerified = student.emailVerified;
                    dbUser.birthDate = student.birthDate;                    


                    var dbStudent = ctx.Students.FirstOrDefault(x => x.userId == student.id) ?? throw new Exception($"Пользователя не существует");

                    dbStudent.Skype = student.Skype;
                    dbStudent.info = student.info;
                    dbStudent.adress = student.adress;
                    dbStudent.lessonsLevelId = student.lessonsLevelId;
                    dbStudent.avatarId = student.avatarId;

                    ctx.SaveChanges();

                    return dbUser.id;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }

        public static long SendRequest(RequestDTO request)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbRequest = ctx.Requests.FirstOrDefault(x => x.id == request.id) ?? ctx.Requests.Add(new DAL.Requests());

                    dbRequest.tutorId = request.tutorId;
                    dbRequest.studentId = request.studentId;
                    dbRequest.subjectId = request.subjectId;
                    dbRequest.lessonsTypeId = request.lessonTypeId;
                    dbRequest.additionalInfo = request.info;
                    dbRequest.status = request.status;
                    dbRequest.date = request.date;                    

                    ctx.SaveChanges();

                    return dbRequest.id;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }
    }
}
