using Core.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CreateToken
{
    public interface IToken
    {
        public Task<string> CreateTokenAsync(AppUser _user, UserManager<AppUser> _userManger);
    }
}
