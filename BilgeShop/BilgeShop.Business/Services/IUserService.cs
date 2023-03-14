using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BilgeShop.Business.Dtos;
using BilgeShop.Business.Types;

namespace BilgeShop.Business.Services
{
    public interface IUserService
    {
        ServiceMessage AddUser(AddUserDto addUserDto);

        UserDto Login(LoginDto loginDto);

    }
}
