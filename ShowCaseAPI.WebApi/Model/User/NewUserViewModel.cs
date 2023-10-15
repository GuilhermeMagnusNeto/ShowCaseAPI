using System.ComponentModel.DataAnnotations.Schema;

namespace ShowCaseAPI.WebApi.Model.User
{
    public class LoginUserViewModel
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
