using Assignment3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages.Authenticate
{
    public class loginModel : PageModel
    {

        private readonly Assignment3.ApplicationContext.PostAppContext _context;

        public loginModel(Assignment3.ApplicationContext.PostAppContext context)
        {
            _context = context;
        }

        private AppUsers user; 

        public JsonResult OnGetLogin(string email, string password)
        {
            System.Diagnostics.Debug.WriteLine("email " + email + " - password " + password);
            if (_context != null)
            {
                user = _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefault();
                if(user != null)
                {
                    return new JsonResult(user);
                }
            }
            return new JsonResult("");
        }
    }

}
