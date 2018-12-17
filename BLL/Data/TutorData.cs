using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data
{
    public static class TutorData
    {
        public static TutorDTO GetTutor(long? id = null, string email = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(email))
                return null;
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutor = ctx.Users.Join(ctx.Tutors,
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
                            education = s.education,
                            qualification = s.qualification,
                            isApproved = s.isApproved,
                            rating = s.rating,
                            birthDate = u.birthDate,
                            regDate = u.regDate

                        }).FirstOrDefault(x => x.id == id || x.email == email);
                    if (dbTutor != null)
                    {
                        var tutor = new TutorDTO
                        {
                            id = dbTutor.id,
                            name = dbTutor.name,
                            email = dbTutor.email,
                            emailVerified = dbTutor.emailVerified,
                            avatarId = dbTutor.avatarId,
                            Skype = dbTutor.Skype,
                            info = dbTutor.info,
                            adress = dbTutor.adress,
                            education = dbTutor.education,
                            qualification = dbTutor.qualification,
                            isApproved = dbTutor.isApproved,
                            rating = dbTutor.rating,
                            birthDate = dbTutor.birthDate,
                            regDate = dbTutor.regDate

                        };

                        return tutor;
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

        public static double updateRating(long tutorId)
        {
            double rating;
            double sum = 0;
             var testimonials = BLL.Data.TutorData.displayTutorTestimonials(tutorId);
            if (testimonials.Count != 0)
            {
                foreach (var t in testimonials)
                {
                    sum += t.star;
                }
                rating = sum / testimonials.Count;
                try
                {
                    using (var ctx = new DAL.tutorDBEntities())
                    {
                        var dbTutor = ctx.Tutors.FirstOrDefault(x => x.userId == tutorId) ?? throw new Exception($"Заявки не существует");
                        dbTutor.rating = rating;
                        ctx.SaveChanges();
                        return rating;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                    //return -1;
                }
            }
            else return 0;

        }

        public static long ChangeRequestStatus(long requestId, string status)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbRequest = ctx.Requests.FirstOrDefault(x => x.id == requestId) ?? throw new Exception($"Заявки не существует");
                    dbRequest.status = status;              

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

        public static long UpdateTutor(TutorDTO tutor)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.id == tutor.id) ?? throw new Exception($"Пользователя не существует");


                    dbUser.name = tutor.name;
                    dbUser.email = tutor.email;
                    //dbUser.emailVerified = student.emailVerified;
                    dbUser.birthDate = tutor.birthDate;


                    var dbTutor = ctx.Tutors.FirstOrDefault(x => x.userId == tutor.id) ?? throw new Exception($"Пользователя не существует");

                    dbTutor.Skype = tutor.Skype;
                    dbTutor.info = tutor.info;
                    dbTutor.adress = tutor.adress;
                    dbTutor.avatarId = tutor.avatarId;

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

        public static List<TutorLessonTypeDTO> GetTutorsLessonTypes(long tutorId)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutorsLessonType = ctx.TutorLessonType.Where(s => s.tutorId == tutorId).Select(dbTLT => new TutorLessonTypeDTO
                    {
                        id = dbTLT.id,
                        tutorId = dbTLT.tutorId,
                        lessontypeId = dbTLT.lessonTypeId,
                        cost = dbTLT.cost
                    }).ToList();
                    if (dbTutorsLessonType != null)
                    {
                        return dbTutorsLessonType;
                    }
                    throw new Exception($"Нет данных");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static long SetTutorsLessonTypes(TutorLessonTypeDTO type)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutorLessonType = ctx.TutorLessonType.FirstOrDefault(x => x.lessonTypeId == type.lessontypeId && x.tutorId == type.tutorId) ?? ctx.TutorLessonType.Add(new DAL.TutorLessonType());


                    dbTutorLessonType.tutorId = type.tutorId;
                    dbTutorLessonType.lessonTypeId = type.lessontypeId;
                    dbTutorLessonType.cost = type.cost;

                    ctx.SaveChanges();

                    return dbTutorLessonType.lessonTypeId;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static bool DeleteTutorsLessonTypes(TutorLessonTypeDTO type)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutorLessonType = ctx.TutorLessonType.SingleOrDefault(x => x.lessonTypeId == type.lessontypeId && x.tutorId == type.tutorId);

                    if (dbTutorLessonType != null)
                    {
                        ctx.TutorLessonType.Remove(dbTutorLessonType);
                    }

                    ctx.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static List<TutorLessonLevelDTO> GetTutorsLessonLevels(long tutorId)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutorsLessonLevel = ctx.TutorLessonLevel.Where(s => s.tutorId == tutorId).Select(dbTLL => new TutorLessonLevelDTO
                    {
                        id = dbTLL.id,
                        tutorId = dbTLL.tutorId,
                        lessonlevelId = dbTLL.lessonLevelId,
                        cost = 0
                    }).ToList();
                    if (dbTutorsLessonLevel != null)
                    {
                        return dbTutorsLessonLevel;
                    }
                    throw new Exception($"Нет данных");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static long SetTutorsLessonLevels(TutorLessonLevelDTO level)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutorLessonLevel = ctx.TutorLessonLevel.FirstOrDefault(x => x.lessonLevelId == level.lessonlevelId && x.tutorId==level.tutorId) ?? ctx.TutorLessonLevel.Add(new DAL.TutorLessonLevel());


                    dbTutorLessonLevel.tutorId = level.tutorId;
                    dbTutorLessonLevel.lessonLevelId = level.lessonlevelId;
                    dbTutorLessonLevel.addCost = 0;

                    ctx.SaveChanges();

                    return dbTutorLessonLevel.lessonLevelId;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static bool DeleteTutorsLessonLevels(TutorLessonLevelDTO level)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutorLessonLevel = ctx.TutorLessonLevel.SingleOrDefault(x => x.lessonLevelId == level.lessonlevelId && x.tutorId == level.tutorId);

                    if (dbTutorLessonLevel != null)
                    {
                        ctx.TutorLessonLevel.Remove(dbTutorLessonLevel);
                    }

                    ctx.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static List<TutorSubjectsDTO> GetTutorsSubjects(long tutorId)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutorSubjects = ctx.TutorSubjects.Join(ctx.Subjects,
                        u => u.subjectId,
                        s => s.id,
                        (u, s) => new
                        {
                            id = u.id,
                            tutorId = u.tutorId,
                            subjectId = u.subjectId,
                            subjectTypeId = s.subjectTypeId,
                            subjectName = s.name

                        }).Where(u => u.tutorId == tutorId).Select(dbTS => new TutorSubjectsDTO
                        {
                            id = dbTS.id,
                            tutorId = dbTS.tutorId,
                            subjectId = dbTS.subjectId,
                            subjectTypeId = dbTS.subjectTypeId,
                            subjectName = dbTS.subjectName
                        }).ToList();
                    if (dbTutorSubjects != null)
                    {
                        return dbTutorSubjects;
                    }
                    throw new Exception($"Нет данных");

                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static long SetTutorsSubjects(TutorSubjectsDTO subject)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutorSubject = ctx.TutorSubjects.FirstOrDefault(x => x.subjectId == subject.subjectId && x.tutorId == subject.tutorId) ?? ctx.TutorSubjects.Add(new DAL.TutorSubjects());


                    dbTutorSubject.tutorId = subject.tutorId;
                    dbTutorSubject.subjectId = subject.subjectId;

                    ctx.SaveChanges();

                    return dbTutorSubject.subjectId;
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static bool DeleteTutorsSubjects(TutorSubjectsDTO subject)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutorSubject = ctx.TutorSubjects.SingleOrDefault(x => x.subjectId == subject.subjectId && x.tutorId == subject.tutorId);

                    if (dbTutorSubject != null)
                    {
                        ctx.TutorSubjects.Remove(dbTutorSubject);
                    }

                    ctx.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static List<TestimonialDTO> displayTutorTestimonials(long tutorId)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbMyTestimonials = ctx.Requests.Join(ctx.Testimonials,
                        u => u.id,
                        s => s.requestId,
                        (u, s) => new
                        {
                            tutorId = u.tutorId,
                            requestId = u.id,                            
                            star = s.star,
                            text = s.text,
                            date = s.date,
                            id = s.id

                        }).Where(u => u.tutorId == tutorId).Select(dbTS => new TestimonialDTO
                        {
                            id = dbTS.id,
                            requestId = dbTS.requestId,
                            star = dbTS.star,
                            text = dbTS.text,
                            date = dbTS.date                            
                        }).ToList();

                    if (dbMyTestimonials != null)
                    {
                        return dbMyTestimonials;
                    }
                    throw new Exception($"Нет данных");
                    
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }

        public static List<RequestDTO> displayStudentsByTutor(long tutorId)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbMyRequests = ctx.Requests.Where(s => s.tutorId == tutorId).Select(mr => new RequestDTO
                    {
                        id = mr.id,
                        studentId = mr.studentId,
                        subjectId = mr.subjectId,
                        lessonTypeId = mr.lessonsTypeId,
                        status = mr.status,
                        info = mr.additionalInfo
                    }).ToList();

                    return dbMyRequests;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }

        public static List<TutorDTO> GetTutors(int page = 0, long selectedType=0, int selectedCost = Int32.MaxValue, int selectedExperience = 0, long selectedSType = 0, long selectedSubject = 0)
        {
            var take = 10;
            var skip = take * page;            
            var res = (List<TutorDTO>)null;
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbTutor = ctx.Users.Join(ctx.Tutors,
                        u => u.id,
                        t => t.userId,
                        (u, t) => new
                            {
                                id = u.id,
                                name = u.name,
                                email = u.email,
                                regDate = u.regDate,
                                birthDate = u.birthDate,
                                avatarId = t.avatarId,
                                Skype = t.Skype,
                                info = t.info,
                                adress = t.adress,
                                education = t.education,
                                qualification = t.qualification,
                                isApproved = t.isApproved,
                                rating = t.rating
                                
                        });

                    if (dbTutor != null)
                    {
                        var tutors = new List<TutorDTO>();
                        foreach (var t in dbTutor)
                        {

                            tutors.Add(new TutorDTO
                            {
                                id = t.id,
                                name = t.name,
                                email = t.email,
                                avatarId = t.avatarId,
                                Skype = t.Skype,
                                info = t.info,
                                adress = t.adress,
                                education = t.education,
                                qualification = t.qualification,
                                isApproved = t.isApproved,
                                rating = t.rating,
                                regDate = t.regDate,
                                birthDate = t.birthDate,                                
                                Subjects = BLL.Data.TutorData.GetTutorsSubjects(t.id),
                                Levels = BLL.Data.TutorData.GetTutorsLessonLevels(t.id),
                                Types = BLL.Data.TutorData.GetTutorsLessonTypes(t.id)
                            });
                        };
                        var qtutors = tutors.AsQueryable();
                        if (selectedType != 0)
                        {
                            qtutors = qtutors.Where(st => st.Types.Any(t => t.lessontypeId == selectedType));
                        }
                        
                        if (selectedCost != Int32.MaxValue)
                        {
                            qtutors = qtutors.Where(st => st.Types.Any(t => t.cost <= selectedCost));
                        }

                        if (selectedExperience != 0)
                        {
                            switch (selectedExperience)
                            {
                                case 1: qtutors = qtutors.Where(st => Convert.ToInt32(st.qualification)<=1); break;
                                case 2: qtutors = qtutors.Where(st => Convert.ToInt32(st.qualification) <= 5 && Convert.ToInt32(st.qualification) >= 2); break;
                                case 3: qtutors = qtutors.Where(st => Convert.ToInt32(st.qualification) <= 10 && Convert.ToInt32(st.qualification) >= 5); break;
                                case 4: qtutors = qtutors.Where(st => Convert.ToInt32(st.qualification) >= 10); break;
                                default: break;
                            }
                            
                        }
                        if (selectedSType != 0)
                        {
                            qtutors = qtutors.Where(st => st.Subjects.Any(t => t.subjectTypeId == selectedSType));
                        }

                        if (selectedSubject != 0)
                        {
                            qtutors = qtutors.Where(st => st.Subjects.Any(t => t.subjectId == selectedSubject));
                        }

                        res = qtutors.OrderByDescending(x => x.regDate).Skip(skip).Take(take).ToList();
                        return res;
                    }

                    throw new Exception($"Преподавателей не найдено");
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
