using Microsoft.EntityFrameworkCore;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Infra.Context.CrossCutting.Identity.Data;
using ShowCaseAPI.Repositories.Base;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Model;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace ShowCaseAPI.Repositories.Extension
{
    public static class StringExtensions
    {
        public static bool IsEmail(this string value)
        {
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(value, pattern);
        }
    }
}
