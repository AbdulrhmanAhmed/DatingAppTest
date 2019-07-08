using System;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Api.Data;
using DatingApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Services
{
    public class AuthService : IAuthRepository
    {
        private readonly DataContext _DataContext;
        public AuthService(DataContext DataContext)
        {
            _DataContext = DataContext;

        }
        public async Task<User> Login(string UserName, string Password)
        {
            var user = await _DataContext.users.FirstOrDefaultAsync();

            if (user == null)
                return null;

            if (!verifyUser(user.PasswordHash, user.PasswordSalt, Password))
                return null;
            return user;
        }

        private bool verifyUser(byte[] passwordHash, byte[] passwordSalt, string password)
        {
            using (var x = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {


                var computed = x.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computed.Length; i++)
                {
                    if (computed[i] != passwordHash[i])
                    {
                        return false;
                    }

                }
                return true;
            }
        }

        public async Task<User> Register(User users, string password)
        {
            byte[] hashPassword, saltPassword;
            createPasswordHash(password, out hashPassword, out saltPassword);
            users.PasswordHash = hashPassword;
            users.PasswordSalt = saltPassword;
            await _DataContext.users.AddAsync(users);
            await _DataContext.SaveChangesAsync();
            return users;
        }

        private void createPasswordHash(string password, out byte[] hashPassword, out byte[] saltPassword)
        {
            using (var x = new System.Security.Cryptography.HMACSHA512())
            {

                saltPassword = x.Key;
                hashPassword = x.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public async Task<bool> UserExist(string UserName)
        {
            if (await _DataContext.users.AnyAsync(x => x.name == UserName))
                return true;
            return false;

        }
    }
}