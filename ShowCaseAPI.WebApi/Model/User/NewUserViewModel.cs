using System.ComponentModel.DataAnnotations.Schema;

namespace ShowCaseAPI.WebApi.Model.User
{
    public class LoginUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
