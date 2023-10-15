using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowCaseAPI.Repositories.Model
{
    public class PasswordHashResult
    {
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
