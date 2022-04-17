using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OOKP_LAB.Entities;
using OOKP_LAB.Models;
using System;
using System.Threading.Tasks;

namespace OOKP_LAB.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: AuthControllercs
        public ActionResult Index()
        {
            return Redirect("auth/registration");
        }

    
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(Registration model)
        {
            try
            {
                var user = new User()
                {
                    Email = model.Email,
                    BirthDate = model.BirthDate,
                    FullName = model.FullName,
                    UserName = model.Email
                };

               
                var result = userManager.CreateAsync(user, model.Password).Result;

                if (result.Succeeded)
                {
                    var token = userManager.GenerateEmailConfirmationTokenAsync(user).Result;

                    await userManager.ConfirmEmailAsync(user, token);

                    await signInManager.SignInAsync(user, true);
                }



                return RedirectToAction("index", "home");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login model)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    return NotFound("User not found!");
                }


                var result = await userManager.CheckPasswordAsync(user, model.Password);
                if (result)
                {
                   await signInManager.SignInAsync(user, true);
                   return RedirectToAction("index", "home");
                } 
                else
                {
                    return View();
                }




            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
