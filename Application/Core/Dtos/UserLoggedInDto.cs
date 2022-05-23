using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Dtos
{
    public class UserLoggedInDto
    {
        public UserForDetailDto User { get; set; }

        public string JwtToken { get; set; }
    }
}
