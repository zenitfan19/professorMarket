using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data
{
    public static class AdditionalData
    {
        public static long GetSubjectCount()
        {
            long dbSubjectCount;
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    dbSubjectCount = ctx.Subjects.AsQueryable().Count();                  
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return dbSubjectCount;
        }

        public static long GetTutorsCount()
        {
            long dbTutorCount;
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    dbTutorCount = ctx.Tutors.AsQueryable().Count();
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            return dbTutorCount;
        }

        public static LessonLevelsDTO GetLessonLevel(long? id = null, string name = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(name))
                return null;

            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbLessonLevel = ctx.LessonLevels.FirstOrDefault(x => x.id == id || x.name == name);
                    if (dbLessonLevel != null)
                    {
                        var lessonLevel = new LessonLevelsDTO
                        {
                            id = dbLessonLevel.id,
                            name = dbLessonLevel.name                            
                        };

                        return lessonLevel;
                    }
                    throw new Exception($"Уровень обучения: {name} не найден");
                }
            }
            catch (Exception ex)
            {
                return null;
            }       

        }

        public static List<LessonLevelsDTO> GetLessonLevels()
        {            
           try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbLessonLevel = ctx.LessonLevels.Select(dbLL => new LessonLevelsDTO
                    {
                        id = dbLL.id,
                        name = dbLL.name
                    }).ToList();
                    if (dbLessonLevel != null)
                    {  
                        return dbLessonLevel;
                    }
                    throw new Exception($"Уровени обучения не обнаружены");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static LessonTypesDTO GetLessonType(long? id = null, string name = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(name))
                return null;

            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbLessonType = ctx.LessonTypes.FirstOrDefault(x => x.id == id || x.name == name);
                    if (dbLessonType != null)
                    {
                        var lessonType = new LessonTypesDTO
                        {
                            id = dbLessonType.id,
                            name = dbLessonType.name
                        };

                        return lessonType;
                    }
                    throw new Exception($"Вариант проведения занятий: {name} не найден");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static List<LessonTypesDTO> GetLessonTypes()
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbLessonType = ctx.LessonTypes.Select(dbLT => new LessonTypesDTO
                    {
                        id = dbLT.id,
                        name = dbLT.name
                    }).ToList();
                    if (dbLessonType != null)
                    {
                        return dbLessonType;
                    }
                    throw new Exception($"Варианты проведения занятий не обнаружены");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static List<SubjectTypesDTO> GetSubjectTypes()
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbSubjectType = ctx.SubjectTypes.Select(dbST => new SubjectTypesDTO
                    {
                        id = dbST.id,
                        name = dbST.name
                    }).ToList();
                    if (dbSubjectType != null)
                    {
                        return dbSubjectType;
                    }
                    throw new Exception($"Группы предметов не обнаружены");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static SubjectTypesDTO GetSubjectType(long? id = null, string name = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(name))
                return null;

            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbSubjectType = ctx.SubjectTypes.FirstOrDefault(x => x.id == id || x.name == name);
                    if (dbSubjectType != null)
                    {
                        var subjectType = new SubjectTypesDTO
                        {
                            id = dbSubjectType.id,
                            name = dbSubjectType.name
                        };

                        return subjectType;
                    }
                    throw new Exception($"Уровень обучения: {name} не найден");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static List<SubjectsDTO> GetAllSubjects()
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbSubject = ctx.Subjects.Select(dbST => new SubjectsDTO
                    {
                        id = dbST.id,
                        name = dbST.name,
                        subjectTypeId = dbST.subjectTypeId
                    }).ToList();
                    if (dbSubject != null)
                    {
                        return dbSubject;
                    }
                    throw new Exception($"Предметы не обнаружены");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static SubjectsDTO GetSubject(long? id = null, string name = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(name))
                return null;

            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbSubject = ctx.Subjects.FirstOrDefault(x => x.id == id || x.name == name);
                    if (dbSubject != null)
                    {
                        var subject = new SubjectsDTO
                        {
                            id = dbSubject.id,
                            name = dbSubject.name,
                            subjectTypeId = dbSubject.subjectTypeId
                        };

                        return subject;
                    }
                    throw new Exception($"Предмет: {name} не найден");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static List<SubjectsDTO> GetTypedSubjects(long subjectTypeId)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbSubject = ctx.Subjects.Where(s => s.subjectTypeId == subjectTypeId).Select(dbST => new SubjectsDTO
                    {
                        id = dbST.id,
                        name = dbST.name,
                        subjectTypeId = dbST.subjectTypeId
                    }).ToList();
                    if (dbSubject != null)
                    {
                        return dbSubject;
                    }
                    throw new Exception($"Предметы не обнаружены");
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static long CreateUpdateSubjects(SubjectsDTO subjects)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbSubject = ctx.Subjects.FirstOrDefault(x => x.id == subjects.id) ?? ctx.Subjects.Add(new DAL.Subjects());

                    if (ctx.Subjects.Any(x => x.name == subjects.name && x.id != dbSubject.id))
                        throw new Exception($"Предмет: {subjects.name} уже существует");


                    dbSubject.name = subjects.name;
                    dbSubject.subjectTypeId = subjects.subjectTypeId;

                    ctx.SaveChanges();

                    return dbSubject.id;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }

        public static long CreateUpdateSubjectTypes(SubjectTypesDTO subjectTypes)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbSubjectType = ctx.SubjectTypes.FirstOrDefault(x => x.id == subjectTypes.id) ?? ctx.SubjectTypes.Add(new DAL.SubjectTypes());

                    if (ctx.SubjectTypes.Any(x => x.name == subjectTypes.name && x.id != dbSubjectType.id))
                        throw new Exception($"Группа предметов: {subjectTypes.name} уже существует");


                    dbSubjectType.name = subjectTypes.name;

                    ctx.SaveChanges();

                    return dbSubjectType.id;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }


        public static long CreateUpdateLessonTypes(LessonTypesDTO lessonTypes)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbLessonType = ctx.LessonTypes.FirstOrDefault(x => x.id == lessonTypes.id) ?? ctx.LessonTypes.Add(new DAL.LessonTypes());

                    if (ctx.LessonTypes.Any(x => x.name == lessonTypes.name && x.id != dbLessonType.id))
                        throw new Exception($"Вариант проведения занятий: {lessonTypes.name} уже существует");


                    dbLessonType.name = lessonTypes.name;

                    ctx.SaveChanges();

                    return dbLessonType.id;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }

        public static long CreateUpdateLessonLevel(LessonLevelsDTO lessonLevels)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbLessonLevel = ctx.LessonLevels.FirstOrDefault(x => x.id == lessonLevels.id) ?? ctx.LessonLevels.Add(new DAL.LessonLevels());

                    if (ctx.LessonLevels.Any(x => x.name == lessonLevels.name && x.id != dbLessonLevel.id))
                        throw new Exception($"Уровень образования: {lessonLevels.name} уже существует");


                    dbLessonLevel.name = lessonLevels.name;                 

                    ctx.SaveChanges();

                    return dbLessonLevel.id;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }

        public static long AddImage(long UserId, ImageWrapper ImageData, bool isDocument)
        {
            using (var ctx = new DAL.tutorDBEntities())
            {
                var img = ctx.Images.Add(new DAL.Images
                {
                    userId = UserId,                    
                    photoContent = ImageData.Content,
                    photoMime = ImageData.Mime,
                    isDocument = isDocument
                });


                ctx.SaveChanges();

                return img.id;
            }
        }

        public static ImageWrapper GetAvatar(long UserId)
        {
            var res = new ImageWrapper();
            using (var ctx = new DAL.tutorDBEntities())
            {
                var dbAvatar = ctx.Images.FirstOrDefault(x => x.id == UserId);
                if (dbAvatar == null)
                    throw new Exception("Avatar not found");

                res.Content = dbAvatar.photoContent;
                res.Mime = dbAvatar.photoMime;
            }

            return res;
        }
    }
}
