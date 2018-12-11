using BLL.DTO;
using Newtonsoft.Json;
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
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Registration()
        {
            var model = new Models.RegistrationModel() { };

            return View(model);
        }

        [HttpPost]
        public ActionResult Registration(Models.RegistrationModel model)
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
                if(res == -1)
                    ModelState.AddModelError("email", "Email уже занят");
                BLL.Data.UserData.ConnectRole(res);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            LoginAfterRegistration(model);
            return RedirectToAction("Index", "Home"); ;
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            var model = new Models.PasswordChangeModel() { };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(Models.PasswordChangeModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            if (BLL.Data.UserData.ValidateUser(user.email, model.oldPassword))
                try
                {
                    var salt = BLL.Hash.CreateSalt(16);
                    var passhash = BLL.Hash.GenerateSaltedHash(model.password, salt);
                    user.passwordSalt = Convert.ToBase64String(salt);
                    user.passwordHash = Convert.ToBase64String(passhash);
                    var res = BLL.Data.UserData.CreateUpdateUser(user);

                    ViewBag.Message = res;
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }
            else
            {
                ModelState.AddModelError("", "Старый пароль введен неверно!");
                return Json(new { error = true, errorMsg = "Старый пароль введен неверно" });
            }


            return Json(new { success = true });
        }

        public ActionResult Avatar(long Id)
        {
               var avatar = BLL.Data.AdditionalData.GetAvatar(Id);            

            if (avatar.Content == null)
                return HttpNotFound();
            return File(avatar.Content, avatar.Mime);
        }



        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.LoginModel model, string ReturnUrl = "")
        {

            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.email, model.password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(model.email, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new Models.CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            Nickname = user.UserName,
                            Email = user.Email,
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, model.email, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("TicketCookie", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Ошибка : Email или пароль неверны");
            return View(model);

        }

        [HttpPost]
        public bool LoginAfterRegistration(Models.RegistrationModel model)
        {

            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.email, model.password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(model.email, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new Models.CustomSerializeModel()
                        {
                            UserId = user.UserId,
                            Nickname = user.UserName,
                            Email = user.Email,
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, model.email, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("TicketCookie", enTicket);
                        Response.Cookies.Add(faCookie);
                    }                  
                   
                        return true;
                    
                }
            }
            
            return false;

        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("TicketCookie", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult SendRequestToAdmin()
        {
            ViewBag.Types = ShowRequestTypes();
            var model = new Models.RequestForAdminModel() { };

            return View(model);
        }
        [Authorize]
        [HttpPost]
        public ActionResult SendRequestToAdmin(Models.RequestForAdminModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = BLL.Data.UserData.GetUser(((CustomPrincipal)User).UserId);
            try
                {
                var res = BLL.Data.AdditionalData.SendRequestForAdmin(new BLL.DTO.RequestForAdminDTO
                {
                    userId = user.id,
                    text = model.text,
                    status = "новая",
                    typeId = model.typeId,
                    date = DateTime.Now,
                    adminId = 9
                });

            }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;
                }

            return Json(new { success = true });
        }
        private IEnumerable<SelectListItem> ShowRequestTypes()
        {
            var dbrequestTypes = BLL.Data.AdditionalData.GetRequestTypes();
            var requestTypes = dbrequestTypes.Select(ll => new SelectListItem()
            {
                Value = ll.id.ToString(),
                Text = ll.name
            });



            return requestTypes;
        }

    }
}