using OOKP_LAB.Models;
using System.Threading.Tasks;

namespace OOKP_LAB.Services.Interfaces
{
    public interface IRegistrationService
    {
        public Task Registration(Registration model);
    }
}
