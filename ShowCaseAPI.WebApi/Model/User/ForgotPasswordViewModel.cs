using System.ComponentModel.DataAnnotations.Schema;

namespace ShowCaseAPI.WebApi.Model.User
{
    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
