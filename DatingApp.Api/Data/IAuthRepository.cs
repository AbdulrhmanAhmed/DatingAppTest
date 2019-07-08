using System.Threading.Tasks;
using DatingApp.Api.Models;

namespace DatingApp.Api.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User users, string password);
        Task<User> Login(string UserName, string Password);
        Task<bool> UserExist(string UserName);
    }
}