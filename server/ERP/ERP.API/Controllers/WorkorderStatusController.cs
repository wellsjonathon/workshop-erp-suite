using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Repositories.Context;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkorderStatusController : ControllerBase
    {
        private readonly WorkorderStatusContext _context;

        public WorkorderStatusController(WorkorderStatusContext context)
        {
            _context = context;
        }

        // GET: api/WorkorderStatus
        [HttpGet]
        public IEnumerable<WorkorderStatus> GetWorkorderStatuses()
        {
            return _context.WorkorderStatuses;
        }

        // GET: api/WorkorderStatus/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkorderStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorderStatus = await _context.WorkorderStatuses.FindAsync(id);

            if (workorderStatus == null)
            {
                return NotFound();
            }

            return Ok(workorderStatus);
        }

        // PUT: api/WorkorderStatus/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkorderStatus([FromRoute] int id, [FromBody] WorkorderStatus workorderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workorderStatus.ID)
            {
                return BadRequest();
            }

            _context.Entry(workorderStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkorderStatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WorkorderStatus
        [HttpPost]
        public async Task<IActionResult> PostWorkorderStatus([FromBody] WorkorderStatus workorderStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.WorkorderStatuses.Add(workorderStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkorderStatus", new { id = workorderStatus.ID }, workorderStatus);
        }

        // DELETE: api/WorkorderStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkorderStatus([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorderStatus = await _context.WorkorderStatuses.FindAsync(id);
            if (workorderStatus == null)
            {
                return NotFound();
            }

            _context.WorkorderStatuses.Remove(workorderStatus);
            await _context.SaveChangesAsync();

            return Ok(workorderStatus);
        }

        private bool WorkorderStatusExists(int id)
        {
            return _context.WorkorderStatuses.Any(e => e.ID == id);
        }
    }
}