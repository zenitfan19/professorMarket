using professorMarketWebUI.CustomAuth;
using professorMarketWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace professorMarketWebUI.Controllers
{
    [Authorize(Roles = "tutor")]
    public class TutorController : Controller
    {
        private TutorProfileModel InitModel()
        {
            var dbTutor = BLL.Data.TutorData.GetTutor(((CustomPrincipal)User).UserId);
            var model = new TutorProfileModel
            {
                Tutor = new TutorModel(dbTutor)
            };
            ViewBag.LessonTypes = BLL.Data.TutorData.GetTutorsLessonTypes(model.Tutor.id);
            ViewBag.LessonLevels = BLL.Data.TutorData.GetTutorsLessonLevels(model.Tutor.id);
            ViewBag.Subjects = BLL.Data.TutorData.GetTutorsSubjects(model.Tutor.id);
            return model;
        }
        // GET: Tutor
        public ActionResult Index()
        {

            return View(InitModel());
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View(InitModel().Tutor);
        }

        [HttpPost]
        public ActionResult Update(Models.TutorModel model, HttpPostedFileBase AvatarImage)
        {

            var tutor = BLL.Data.TutorData.GetTutor(model.id);
            if (AvatarImage != null)
            {
                tutor.avatarId = BLL.Data.AdditionalData.AddImage(model.id, new BLL.DTO.ImageWrapper(AvatarImage), false);
            }
            tutor.name = model.name;
            tutor.email = model.email;
            tutor.birthDate = model.birthDate;
            tutor.Skype = model.Skype;
            tutor.info = model.info;
            tutor.adress = model.adress;            
            //student.regDate = model.regDate;

            BLL.Data.TutorData.UpdateTutor(tutor);


            return Redirect("/Tutor/Index");
        }

        [HttpPost]
        public ActionResult SetRequestStatus(long id, string status)
        {
            try
                {
                    var res = BLL.Data.TutorData.ChangeRequestStatus(id, status);                   
                }
                catch (Exception ex)
                {
                return Json(new { error = true });
            }        


            return Json(new { success = true });
        }

        public ActionResult ShowMyStudents()
        {
            var myStudents = BLL.Data.TutorData.displayStudentsByTutor(((CustomPrincipal)User).UserId);
            foreach (var s in myStudents)
            {
                s.student = BLL.Data.StudentData.GetStudent(s.studentId);
            }
            return View(myStudents);
        }

        [HttpGet]
        public ActionResult SetLessonLevels()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult ShowLessonLevelsIds()
        {
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            var level = BLL.Data.TutorData.GetTutorsLessonLevels(user.id);
            List<long> levelIds = new List<long>(); 
            if (level.Any())
                try
                {
                    foreach (var l in level)
                    {
                        levelIds.Add(l.lessonlevelId);
                    }                   
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            else
            {
                return Json(new { error = true });
            }


            return Json(new { success = true, ids = levelIds });
        }

        [HttpPost]
        public ActionResult SetLessonLevels(long id)
        {
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            var level = BLL.Data.AdditionalData.GetLessonLevel(id);
            if (level != null)
                try
                {
                    var res = BLL.Data.TutorData.SetTutorsLessonLevels(new BLL.DTO.TutorLessonLevelDTO
                    {
                        tutorId = user.id,
                        lessonlevelId = level.id
                    });

                    ViewBag.Message = res;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            else
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult DelLessonLevels(long id)
        {
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            var level = BLL.Data.AdditionalData.GetLessonLevel(id);
            if (level != null)
                try
                {
                    var res = BLL.Data.TutorData.DeleteTutorsLessonLevels(new BLL.DTO.TutorLessonLevelDTO
                    {
                        tutorId = user.id,
                        lessonlevelId = level.id
                    });

                    ViewBag.Message = res;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return Json(new { error = true, errorMsg = "Произошла ошибка" });
                }
            else
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true });
        }

        [HttpGet]
        public ActionResult SetLessonTypes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowLessonTypesIds()
        {
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            var type = BLL.Data.TutorData.GetTutorsLessonTypes(user.id);            
            if (type.Any())
                return Json(new { success = true, ids = type });
            else
            {
                return Json(new { error = true });
            }            
        }
        [HttpPost]
        public ActionResult DelLessonTypes(long id)
        {
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            var type = BLL.Data.AdditionalData.GetLessonType(id);
            if (type != null)
                try
                {
                    var res = BLL.Data.TutorData.DeleteTutorsLessonTypes(new BLL.DTO.TutorLessonTypeDTO
                    {
                        tutorId = user.id,
                        lessontypeId = type.id
                    });

                    ViewBag.Message = res;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return Json(new { error = true, errorMsg = "Произошла ошибка" });
                }
            else
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult SetLessonTypes(long id, int cost)
        {
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            var type = BLL.Data.AdditionalData.GetLessonType(id);
            if (type != null)
                try
                {
                    var res = BLL.Data.TutorData.SetTutorsLessonTypes(new BLL.DTO.TutorLessonTypeDTO
                    {
                        tutorId = user.id,
                        lessontypeId = type.id,
                        cost = cost

                    });

                    ViewBag.Message = res;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            else
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true });
        }


        [HttpGet]
        public ActionResult SetSubjects()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ShowSubjectsIds()
        {
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            var subject = BLL.Data.TutorData.GetTutorsSubjects(user.id);
            if (subject.Any())
                return Json(new { success = true, ids = subject });
            else
            {
                return Json(new { error = true });
            }
        }
        [HttpPost]
        public ActionResult DelSubjects(long id)
        {
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            var subject = BLL.Data.AdditionalData.GetSubject(id);
            if (subject != null)
                try
                {
                    var res = BLL.Data.TutorData.DeleteTutorsSubjects(new BLL.DTO.TutorSubjectsDTO
                    {
                        tutorId = user.id,
                        subjectId = subject.id
                    });

                    ViewBag.Message = res;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                    return Json(new { error = true, errorMsg = "Произошла ошибка" });
                }
            else
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult SetSubjects(long id)
        {
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            var subject = BLL.Data.AdditionalData.GetSubject(id);
            if (subject != null)
                try
                {
                    var res = BLL.Data.TutorData.SetTutorsSubjects(new BLL.DTO.TutorSubjectsDTO
                    {
                        tutorId = user.id,
                        subjectId = subject.id                       

                    });

                    ViewBag.Message = res;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            else
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true });
        }

    }
}