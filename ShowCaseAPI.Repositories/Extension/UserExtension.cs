using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Model;
using System.Security.Cryptography;

namespace ShowCaseAPI.Repositories.Extension
{
    public static class UserExtension
    {
        public static PasswordHashResult CreatePasswordHash(this string password)
        {
            using var hmac = new HMACSHA512();
            byte[] passwordSalt = hmac.Key;
            byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return new PasswordHashResult
            {
                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = Convert.ToBase64String(passwordSalt)  
            };
        }
    }
}
