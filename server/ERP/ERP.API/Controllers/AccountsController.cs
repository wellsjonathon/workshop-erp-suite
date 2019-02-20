using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Route("~/account")]
    [AllowAnonymous]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        [HttpPost("login")]
    }
}