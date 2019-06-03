using HeLian.Xiaoyi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeLian.Xiaoyi.UserService.NavtiveControllers
{
    [Route("native/{controller}")]
    public class UserController:ControllerBase
    {
        [HttpGet("{username}")]
        public async Task<ActionResult<UserMo>> GetUser(string username)
        {
            if (username == "admin")
            {
                return new UserMo() { Id = 1 , Role="admin"};
            }
            return NotFound();
        }
    }
}
