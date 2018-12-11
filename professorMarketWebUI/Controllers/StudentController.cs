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
            var model = new Models.RequestModel() { };            
            return View(model);
        }

        public ActionResult ShowMyTutors()
        {
            var myTutors = BLL.Data.StudentData.displayTutorsByStudent(((CustomPrincipal)User).UserId);
            foreach (var t in myTutors)
            {
                t.tutor = BLL.Data.TutorData.GetTutor(t.tutorId);
            }
            return View(myTutors);
        }

        [HttpPost]
        public ActionResult SendRequestToTutor(Models.RequestModel model)
        {
            
            var student = BLL.Data.StudentData.GetStudent(((CustomPrincipal)User).UserId);
            try
            {
                var res = BLL.Data.StudentData.SendRequest(new BLL.DTO.RequestDTO
                {
                    studentId = student.id,
                    lessonTypeId = model.lessonTypeId,
                    info = model.info,
                    status = "",
                    subjectId = model.subjectId,
                    date = DateTime.Now,
                    tutorId = model.tutorId
                    
                });

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return Json(new { success = true });
        }


        [HttpGet]
        public ActionResult ShowTutors(int page = 0, long selectedSubject = 0)
        {
            var model = new Models.TutorFinderModel();
            
            model.NextExist = false;
            model.types = BLL.Data.AdditionalData.GetLessonTypes();
            model.sTypes = BLL.Data.AdditionalData.GetSubjectTypes();
            model.subjects = BLL.Data.AdditionalData.GetAllSubjects();
            model.selectedSubject = selectedSubject;
            model.Tutors = BLL.Data.TutorData.GetTutors(page, 0, Int32.MaxValue, 0, 0, selectedSubject);
            return View(model);
        }

        [HttpPost]
        public ActionResult FindTutors(int page = 0, long selectedType=0, int selectedCost = Int32.MaxValue, int selectedExperience = 0, long selectedSType = 0, long selectedSubject = 0)
        {
            var model = new Models.TutorFinderModel();
            model.Tutors = BLL.Data.TutorData.GetTutors(0, selectedType, selectedCost, selectedExperience, selectedSType, selectedSubject);
            model.NextExist = false;                        
            return View("ShowTutor", model.Tutors);
        }

               
    }
}