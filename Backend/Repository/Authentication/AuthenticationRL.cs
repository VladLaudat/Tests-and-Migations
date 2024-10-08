using Backend.Controllers.RequestModels;
using Backend.Controllers.ResponseModels;
using Backend.DbContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Backend.Service.Authentication
{
    public class AuthenticationRL : IAuthenticationRL
    {
        private readonly BackendDBContext _dBContext;

        public AuthenticationRL(BackendDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public User Login(string Username, string Password)
        {
            User user = _dBContext.Users.FirstOrDefault(u => u.UserName == Username && u.Password == Password);

            return user;
        }

        public User Signup(User user)
        {
            User alreadyExistingUser = _dBContext.Users.FirstOrDefault(u => u.UserName == user.UserName || u.Email == user.Email);

            if (alreadyExistingUser != null)
                return null;

            try
            {
                _dBContext.Add(user);
                _dBContext.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        string IAuthenticationRL.GetPassword(string email)
        {
            User user = _dBContext.Users.FirstOrDefault(u => u.Email == email);
            if(user!=null)
                return user.Password;
            return null;
        }
        bool IAuthenticationRL.SetPassword(string email, string newPassword)
        {
            if (newPassword == null) return false;
            User user = _dBContext.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
                { user.Password = newPassword; _dBContext.SaveChanges(); return true; }
            return false;
        }

        string IAuthenticationRL.GetUserName(string email)
        {
            User user = _dBContext.Users.FirstOrDefault(u => u.Email == email);
            if (user!=null) 
                return user.UserName;
            return null;
        }
    }
}
