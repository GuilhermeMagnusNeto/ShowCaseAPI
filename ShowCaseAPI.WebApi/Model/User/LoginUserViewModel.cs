using System.ComponentModel.DataAnnotations.Schema;

namespace ShowCaseAPI.WebApi.Model.User
{
    public class NewUserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
