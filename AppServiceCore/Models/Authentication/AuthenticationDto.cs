using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceCore.Models.Authentication
{
    public class AuthenticationDto
    {
        public Guid UserAccountId { get; set; }
        public string Login { get; set; } = string.Empty;
        public bool UserLoginInactive { get; set; } = false;
        public bool UserAccountInactive { get; set; } = false;  
        public string UserFirstName { get; set; } = string.Empty;
        public string UserLastName { get; set; } = string.Empty;
    }
}
