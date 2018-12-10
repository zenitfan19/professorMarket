using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Data
{
    public static class UserData
    {
        public static long CreateUpdateUser(UserDTO user)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.id == user.id) ?? ctx.Users.Add(new DAL.Users());

                    if (ctx.Users.Any(x => x.email == user.email && x.id != dbUser.id))
                        return -1;


                    dbUser.name = user.name;
                    dbUser.email = user.email;
                    dbUser.passwordHash = user.passwordHash;
                    dbUser.passwordSalt = user.passwordSalt;
                    dbUser.emailVerified = user.emailVerified;
                    dbUser.birthDate = user.birthDate;
                    dbUser.regDate = user.regDate;
                    dbUser.role = user.role;

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

        public static long ConnectRole(long id)
        {
            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    if (ctx.Users.FirstOrDefault(x => x.id == id).role == "student")
                    {
                        var dbStudent = ctx.Students.FirstOrDefault(x => x.userId == id) ?? ctx.Students.Add(new DAL.Students());
                        dbStudent.userId = id;
                        ctx.SaveChanges();
                    }
                    if (ctx.Users.FirstOrDefault(x => x.id == id).role == "tutor")
                    {
                        var dbTutor = ctx.Tutors.FirstOrDefault(x => x.userId == id) ?? ctx.Tutors.Add(new DAL.Tutors());
                        dbTutor.userId = id;
                        ctx.SaveChanges();
                    }                                     

                    return id;
                }
            }
            catch (Exception ex)
            {
                throw;
                //return -1;
            }

        }

        public static string[] GetRole(string email)
        {
            string[] roles = new string[] { };
            using (var ctx = new DAL.tutorDBEntities())
            {
                // Получаем пользователя
                var user = ctx.Users.FirstOrDefault(u => u.email == email);
                if (user != null && user.role != null)
                {
                    // получаем роль
                    roles = new string[] { user.role };
                }
                return roles;
            }
        }

        public static bool IsUserInRole(string email, string role)
        {
            using (var ctx = new DAL.tutorDBEntities())
            {
                // Получаем пользователя
                var user = ctx.Users.FirstOrDefault(u => u.email == email);
                if (user != null && user.role != null && user.role == role)
                    return true;
                else
                    return false;
            }
        }

        public static UserDTO GetUser(long? id = null, string email = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(email))
                return null;

            try
            {
                using (var ctx = new DAL.tutorDBEntities())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.id == id || x.email == email);
                    if (dbUser != null)
                    {
                        var user = new UserDTO
                        {
                            id = dbUser.id,
                            name = dbUser.name,
                            email = dbUser.email,
                            passwordHash = dbUser.passwordHash,
                            passwordSalt = dbUser.passwordSalt,
                            emailVerified = dbUser.emailVerified,
                            birthDate = dbUser.birthDate,
                            regDate = dbUser.regDate,
                            role = dbUser.role
                        };

                        return user;
                    }
                    throw new Exception($"Пользователь с email: {email} не найден");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }       

        public static bool ValidateUser(string email, string password)
        {
            var user = BLL.Data.UserData.GetUser(email: email);
            if (user != null)
            {
                var salt = Convert.FromBase64String(user.passwordSalt);
                var passhash = BLL.Hash.GenerateSaltedHash(password, salt);

                var oldHash = Convert.FromBase64String(user.passwordHash);

                return BLL.Hash.CompareByteArrays(passhash, oldHash);
            }
            return false;
        }

        public static int GetAge(DateTime birthDate)
        {
            var now = DateTime.Today;
            return now.Year - birthDate.Year - 1 +
                ((now.Month > birthDate.Month || now.Month == birthDate.Month && now.Day >= birthDate.Day) ? 1 : 0);
        }
    }
}
