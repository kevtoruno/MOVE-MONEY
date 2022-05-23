using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Interface
{
    public interface IJwtTokenCreator
    {
        string GenerateJwtToken(Claim[] claims);
    }
}
