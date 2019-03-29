using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Models.Workflows;
using ERP.Repositories.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkflowController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("states")]
        public IEnumerable<State> GetAllStates()
        {
            return _context.States;
        }
    }
}