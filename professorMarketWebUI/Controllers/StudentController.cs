using professorMarketWebUI.CustomAuth;
using professorMarketWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace professorMarketWebUI.Controllers
{
    
    [Authorize(Roles = "student")]
    public class StudentController : Controller
    {
        // GET: Student
        private StudentProfileModel InitModel()
        {
            var dbStudent = BLL.Data.StudentData.GetStudent(((CustomPrincipal)User).UserId);
            var model = new StudentProfileModel
            {
                Student = new StudentModel(dbStudent)                
            };
            ViewBag.Levels = ShowLessonLevels();
            ViewBag.LevelName = BLL.Data.AdditionalData.GetLessonLevel(model.Student.lesonsLevel).name;            

            return model;
        }

        private IEnumerable<SelectListItem> ShowLessonLevels()
        {
            var dbLessonlevels = BLL.Data.AdditionalData.GetLessonLevels();
            var lessonlevels = dbLessonlevels.Select(ll => new SelectListItem()
                {
                  Value = ll.id.ToString(),
                  Text = ll.name
                });
           


            return lessonlevels;
        }

        // GET: UserProfile
        public ActionResult Index()
        {

            return View(InitModel());
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View(InitModel().Student);
        }       

        [HttpPost]
        public ActionResult Update(Models.StudentModel model, HttpPostedFileBase AvatarImage)
        {
            
            ViewBag.Levels = ShowLessonLevels();
            var student = BLL.Data.StudentData.GetStudent(model.id);
            if (AvatarImage != null)
            {
                student.avatarId = BLL.Data.AdditionalData.AddImage(model.id, new BLL.DTO.ImageWrapper(AvatarImage), false);                
            }
            student.name = model.name;
            student.email = model.email;
            student.birthDate = model.birthDate;
            student.Skype = model.Skype;
            student.info = model.info;
            student.adress = model.adress;
            student.lessonsLevelId = model.lesonsLevel;
            //student.regDate = model.regDate;

            BLL.Data.StudentData.UpdateStudent(student);            


            return Redirect("/Student/Index");
        }

        public ActionResult SendRequestToTutor()
        {
            var model = new Models.RegistrationModel() { };

            return View(model);
        }

        [HttpPost]
        public ActionResult SendRequestToTutor(Models.RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var salt = BLL.Hash.CreateSalt(16);
                var passhash = BLL.Hash.GenerateSaltedHash(model.password, salt);
                var res = BLL.Data.UserData.CreateUpdateUser(new BLL.DTO.UserDTO
                {
                    passwordSalt = Convert.ToBase64String(salt),
                    passwordHash = Convert.ToBase64String(passhash),
                    email = model.email,
                    name = model.name,
                    regDate = DateTime.Now,
                    birthDate = model.birthDate,
                    emailVerified = true,
                    role = model.role
                });
                if (res == -1)
                    ModelState.AddModelError("email", "Email уже занят");
                BLL.Data.UserData.ConnectRole(res);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }            
            return RedirectToAction("Index", "Home"); ;
        }



        public ActionResult ShowTutors(int page=0)
        {
            var model = new Models.TutorFinderModel();
            model.Tutors = BLL.Data.TutorData.GetTutors(page);
            model.NextExist = false;
            model.types = BLL.Data.AdditionalData.GetLessonTypes();
            model.sTypes = BLL.Data.AdditionalData.GetSubjectTypes();
            return View(model);
        }

        //[HttpPost]
        //public ActionResult ShowTutorsBySubjects(long subjectId)
        //{

        //    ViewBag.Levels = ShowLessonLevels();
        //    var student = BLL.Data.StudentData.GetStudent(model.id);
        //    if (AvatarImage != null)
        //    {
        //        student.avatarId = BLL.Data.AdditionalData.AddImage(model.id, new BLL.DTO.ImageWrapper(AvatarImage), false);
        //    }
        //    student.name = model.name;
        //    student.email = model.email;
        //    student.birthDate = model.birthDate;
        //    student.Skype = model.Skype;
        //    student.info = model.info;
        //    student.adress = model.adress;
        //    student.lessonsLevelId = model.lesonsLevel;
        //    //student.regDate = model.regDate;

        //    BLL.Data.StudentData.UpdateStudent(student);


        //    return Redirect("/Student/Index");
        //}

        //[HttpPost]
        //public ActionResult ShowTutorsByLessonLevels(long lessonLevelId)
        //{

        //    ViewBag.Levels = ShowLessonLevels();
        //    var student = BLL.Data.StudentData.GetStudent(model.id);
        //    if (AvatarImage != null)
        //    {
        //        student.avatarId = BLL.Data.AdditionalData.AddImage(model.id, new BLL.DTO.ImageWrapper(AvatarImage), false);
        //    }
        //    student.name = model.name;
        //    student.email = model.email;
        //    student.birthDate = model.birthDate;
        //    student.Skype = model.Skype;
        //    student.info = model.info;
        //    student.adress = model.adress;
        //    student.lessonsLevelId = model.lesonsLevel;
        //    //student.regDate = model.regDate;

        //    BLL.Data.StudentData.UpdateStudent(student);


        //    return Redirect("/Student/Index");
        //}

        //[HttpPost]
        //public ActionResult ShowTutorsByLessonTypes(long lessonTypeId)
        //{

        //    ViewBag.Levels = ShowLessonLevels();
        //    var student = BLL.Data.StudentData.GetStudent(model.id);
        //    if (AvatarImage != null)
        //    {
        //        student.avatarId = BLL.Data.AdditionalData.AddImage(model.id, new BLL.DTO.ImageWrapper(AvatarImage), false);
        //    }
        //    student.name = model.name;
        //    student.email = model.email;
        //    student.birthDate = model.birthDate;
        //    student.Skype = model.Skype;
        //    student.info = model.info;
        //    student.adress = model.adress;
        //    student.lessonsLevelId = model.lesonsLevel;
        //    //student.regDate = model.regDate;

        //    BLL.Data.StudentData.UpdateStudent(student);


        //    return Redirect("/Student/Index");
        //}
    }
}