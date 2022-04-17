using Microsoft.AspNetCore.Identity;
using System;

namespace OOKP_LAB.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
