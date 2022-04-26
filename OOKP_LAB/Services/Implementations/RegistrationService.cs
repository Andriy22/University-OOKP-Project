using Microsoft.AspNetCore.Identity;
using OOKP_LAB.Entities;
using OOKP_LAB.Models;
using OOKP_LAB.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OOKP_LAB.Services.Implementations
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public RegistrationService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task Registration(Registration model)
        {
            var user = new User()
            {
                Email = model.Email,
                BirthDate = model.BirthDate,
                FullName = model.FullName,
                UserName = model.Email
            };


            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                await userManager.ConfirmEmailAsync(user, token);

                await signInManager.SignInAsync(user, true);
            }
            else
            {
                throw new Exception(result.Errors.FirstOrDefault()?.Description);
            }
        }
    }
}
