using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OOKP_LAB.Entities;
using OOKP_LAB.Models;
using OOKP_LAB.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace OOKP_LAB.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IRegistrationService registrationService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IRegistrationService registrationService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.registrationService = registrationService;
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
                await registrationService.Registration(model);

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

        public ActionResult LogOut()
        {
            this.signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
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
                    if (await userManager.IsEmailConfirmedAsync(user))
                    {
                        await signInManager.SignInAsync(user, true);
                        return RedirectToAction("index", "home");
                    }

                    return BadRequest("User email not confirmed");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
