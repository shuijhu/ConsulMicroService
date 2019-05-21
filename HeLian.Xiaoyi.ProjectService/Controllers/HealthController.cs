using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeLian.Xiaoyi.ProjectService.Controllers
{
    [Produces("application/json")]
    [Route("api/Health")]
    public class HealthController:ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok("ok");
    }
}
