using Microsoft.AspNetCore.Mvc;
using ShowCaseAPI.Domain.Entities;
using ShowCaseAPI.Repositories.Extension;
using ShowCaseAPI.Repositories.Interface;
using ShowCaseAPI.Repositories.Repository;
using ShowCaseAPI.WebApi.Model.Product;
using ShowCaseAPI.WebApi.Model.User;
using System.Net;

namespace ShowCaseAPI.WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterNewUser(NewUserViewModel vm)
        {
            try
            {
                //TODO: VALIDAÇÃO FORMATO EMAIL
                var hash = vm.Password.CreatePasswordHash();
                var user = new User
                {
                    Name = vm.Name,
                    PasswordHash = hash.PasswordHash,
                    PasswordSalt = hash.PasswordSalt,
                    Email = vm.Email.ToUpper()
                };

                var result = await _userRepository.Insert(user);
                if (result > 0)
                {
                    return Ok("Usuário criado com sucesso!");
                }
                return BadRequest("Ocorreu um erro durante o registro do usuário.");
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserViewModel vm)
        {
            try
            {
                var user = await _userRepository.GetByEmail(vm.Email);
                if (user == null)
                {
                    return BadRequest("Email não encontrado!");
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, e.Message);
            }
        }
    }
}
