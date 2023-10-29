using System.ComponentModel.DataAnnotations.Schema;

namespace ShowCaseAPI.WebApi.Model.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
