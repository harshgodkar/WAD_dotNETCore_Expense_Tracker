using ExpenseTracker.AppDbContext;
using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class UserProfileController : Controller
    {
        ExpensesDataAcessLayer objexpense;

        public UserProfileController(ExpensesDataAcessLayer objexpense)
        {
            this.objexpense = objexpense;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string Email, string Password) 
        {
            ViewBag.LoginStatus = "";


            if(ModelState.IsValid)
            {
                var userCheck = objexpense.getUserByEmailPassword(Email, Password);
                if(userCheck == null)
                {
                    ViewBag.LoginStatus = "Invalid Login or User not Registered";
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Registration(UserProfile userProfile)
        {

            if (ModelState.IsValid)
            {
                objexpense.AddUser(userProfile);
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
