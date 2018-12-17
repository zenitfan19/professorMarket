using professorMarketWebUI.CustomAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace professorMarketWebUI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.Types = ShowSubjectTypes();
            return View();
        }

        public ActionResult ShowMyRequests()
        {
            var myRequests = BLL.Data.AdminData.displayRequestsForAdmin(((CustomPrincipal)User).UserId);
            foreach (var s in myRequests)
            {
                s.user = BLL.Data.UserData.GetUser(s.userId);
            }
            return View(myRequests);
        }

        public ActionResult SetSiteParameters()
        {
            return View("SetSiteParameters");
        }


        private IEnumerable<SelectListItem> ShowSubjectTypes()
        {
            var dbSubjectTypes = BLL.Data.AdditionalData.GetSubjectTypes();
            var subjecttypes = dbSubjectTypes.Select(st => new SelectListItem()
            {
                Value = st.id.ToString(),
                Text = st.name
            });



            return subjecttypes;
        }

        public ActionResult AddLessonLevel()
        {
            var model = new Models.LessonLevelsModel() { };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddLessonLevel(Models.LessonLevelsModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { error = true, errorMsg = "Введите корректное значение" });
            try
            {
               var res = BLL.Data.AdditionalData.CreateUpdateLessonLevel(new BLL.DTO.LessonLevelsDTO
                {
                    name = model.name                    
                });                
                ViewBag.Message = res;
            }
            catch (Exception ex)
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true, llName = model.name });
        }

        public ActionResult AddLessonTypes()
        {
            var model = new Models.LessonTypesModel() { };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddLessonTypes(Models.LessonTypesModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { error = true, errorMsg = "Введите корректное значение" });
            try
            {
                var res = BLL.Data.AdditionalData.CreateUpdateLessonTypes(new BLL.DTO.LessonTypesDTO
                {
                    name = model.name
                });
                ViewBag.Message = res;
            }
            catch (Exception ex)
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true, ltName = model.name });
        }

        public ActionResult AddSubjectTypes()
        {
            var model = new Models.SubjectTypesModel() { };

            return View(model);
        }

        [HttpPost]
        public ActionResult AddSubjectTypes(Models.SubjectTypesModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { error = true, errorMsg = "Введите корректное значение" });
            try
            {
                var res = BLL.Data.AdditionalData.CreateUpdateSubjectTypes(new BLL.DTO.SubjectTypesDTO
                {
                    name = model.name
                });
                ViewBag.Message = res;
            }
            catch (Exception ex)
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true, stName = model.name });
        }


        public ActionResult AddSubject()
        {
            ViewBag.Types = ShowSubjectTypes();
            var model = new Models.SubjectModel() { };
            

            return View(model);
        }

        [HttpPost]
        public ActionResult AddSubject(Models.SubjectModel model)
        {
            if (!ModelState.IsValid)
                return Json(new { error = true, errorMsg = "Введите корректное значение" });
            try
            {
                var res = BLL.Data.AdditionalData.CreateUpdateSubjects(new BLL.DTO.SubjectsDTO
                {
                    name = model.name,
                    subjectTypeId = model.subjectTypeId
                });
                ViewBag.Message = res;
            }
            catch (Exception ex)
            {
                return Json(new { error = true, errorMsg = "Произошла ошибка" });
            }


            return Json(new { success = true, stName = model.name, stTypeName = BLL.Data.AdditionalData.GetSubjectType(model.subjectTypeId).name });
        }




    }
}