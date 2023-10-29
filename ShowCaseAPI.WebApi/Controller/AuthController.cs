using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Extension;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.WebApi.Helper;
using ShowCaseAPI.WebApi.Model.User;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser([FromBody] NewUserViewModel vm)
        {
            try
            {
                if (!vm.Email.IsEmail())
                {
                    return ResponseHelper.BadRequest("Formato de email incorreto!");
                }

                if (_userRepository.Query().FirstOrDefault(x => x.Email.ToUpper() == vm.Email.ToUpper()) != null)
                {
                    return ResponseHelper.BadRequest("Email já cadastrado!");
                }
                var hash = vm.Password.CreatePasswordHash();
                var user = new User
                {
                    PasswordHash = hash.PasswordHash,
                    PasswordSalt = hash.PasswordSalt,
                    Email = vm.Email.ToUpper()
                };

                var result = await _userRepository.Insert(user);
                if (result > 0)
                {
                    string token = CreateToken(user);
                    return ResponseHelper.Success(new UserViewModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Token = token
                    });
                }

                return ResponseHelper.BadRequest();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserViewModel vm)
        {
            try
            {
                var user = await _userRepository.GetByEmail(vm.Email);
                if (user == null)
                {
                    return ResponseHelper.BadRequest("Email não encontrado!");
                }

                if (!vm.Password.VerifyPasswordHash(user.PasswordHash, user.PasswordSalt))
                {
                    return ResponseHelper.BadRequest("Senha incorreta!");
                }

                string token = CreateToken(user);
                return ResponseHelper.Success(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Token = token
                });
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }

        /// <summary>
        /// FOR TEST!
        /// </summary>
        /// <returns></returns>
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel vm)
        {
            try
            {
                var user = await _userRepository.GetByEmail(vm.Email);
                if (user == null)
                {
                    return ResponseHelper.BadRequest("Email não encontrado!");
                }

                var hash = vm.NewPassword.CreatePasswordHash();

                user.PasswordHash = hash.PasswordHash;
                user.PasswordSalt = hash.PasswordSalt;

                var result = await _userRepository.Update(user);
                return ResponseHelper.Success();
            }
            catch (Exception e)
            {
                return ResponseHelper.InternalServerError(e.Message);
            }
        }


        //[HttpPost("ForgotPassword")]
        //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel vm)
        //{
        //    try
        //    {
        //        var user = await _userRepository.GetByEmail(vm.Email);
        //        if (user == null)// || !user.EmailConfirmed)
        //        {
        //            return BadRequest("Email não encontrado ou confirmado!");
        //        }

        //        //TODO: Fazer reset correto de senha!
        //        //var codeReset = await _userRepository.GetByEmail(user);

        //        var hash = vm.NewPassword.CreatePasswordHash();

        //        user.PasswordHash = hash.PasswordHash;
        //        user.PasswordSalt = hash.PasswordSalt;

        //        var result = await _userRepository.Update(user);
        //        return Ok("Senha alterada com sucesso!");
        //    }
        //    catch (Exception e)
        //    {
        //        return ResponseHelper.InternalServerError(e.Message);
        //    }
        //}


        #region _FUNCTIONS
        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        #endregion //_FUNCTIONS
    }
}
