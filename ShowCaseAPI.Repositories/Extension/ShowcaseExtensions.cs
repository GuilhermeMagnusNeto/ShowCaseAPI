using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Model;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace ShowCaseAPI.Repositories.Extension
{
    public static class ShowcaseExtension
    {
        public static string GenerateRandomCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            StringBuilder code = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                code.Append(chars[random.Next(chars.Length)]);
            }

            return code.ToString();
        }
    }
}
