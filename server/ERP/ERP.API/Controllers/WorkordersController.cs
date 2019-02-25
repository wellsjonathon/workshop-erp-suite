using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Models.Workorders;
using ERP.Repositories.Context;
using Microsoft.AspNetCore.Authorization;
using ERP.Models.Workflows;

// TODO: Delegate any calls that needs to build the object to the
//         services - allows use of multiple contexts

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    [ApiController]
    public class WorkordersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkordersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Workorders
        [HttpGet]
        public IEnumerable<Workorder> GetWorkorders()
        {
            //var workorders = _context.Workorders;
            //foreach (Workorder w in workorders)
            //{
            //    w.Status = _context.WorkorderStatuses.FirstOrDefaultAsync(s => s.Id == w.Status.Id);
            //}
            return _context.Workorders.Include(w => w.State);
        }

        // GET: api/Workorders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkorder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders.Include(w => w.State).FirstOrDefaultAsync(w => w.Id == id);

            if (workorder == null)
            {
                return NotFound();
            }

            return Ok(workorder);
        }

        // PUT: api/Workorders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkorder([FromRoute] int id, [FromBody] Workorder workorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workorder.Id)
            {
                return BadRequest();
            }

            _context.Entry(workorder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkorderExists(id))
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

        // POST: api/Workorders
        [HttpPost]
        public async Task<IActionResult> PostWorkorder([FromBody] Workorder workorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            workorder.State = await _context.States.FirstOrDefaultAsync(x => x.Id == 1);
            workorder.DateCreated = DateTime.Now;

            _context.Workorders.Add(workorder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkorder", new { id = workorder.Id }, workorder);
        }

        // DELETE: api/Workorders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkorder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders.FindAsync(id);
            if (workorder == null)
            {
                return NotFound();
            }

            _context.Workorders.Remove(workorder);
            await _context.SaveChangesAsync();

            return Ok(workorder);
        }

        private bool WorkorderExists(int id)
        {
            return _context.Workorders.Any(e => e.Id == id);
        }

        // ===== TRANSITIONS =====

        // GET: api/Workorders/5/transitions
        [HttpGet("{id}/transitions")]
        public IActionResult GetWorkorderTransitions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = _context.Workorders.FirstOrDefault(w => w.Id == id);

            if (id != workorder.Id)
            {
                return BadRequest();
            }

            // Determine possible transitions from current state of the workorder
            var transitions = _context.Transitions
                .Where(t => t.CurrentStateId == workorder.State.Id)
                .OrderBy(t => t.NextStateId)
                .ToList();

            return Ok(transitions);
        }

        // POST: api/Workorders/5/transitions
        [HttpPost("{id}/transitions")]
        public async Task<IActionResult> PostWorkorderTransition([FromRoute] int id, [FromBody] Transition transition)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders.FirstOrDefaultAsync(w => w.Id == id);

            if (id != workorder.Id)
            {
                return BadRequest();
            }

            // Determine possible transitions from current state of the workorder
            var transitionObj = _context.Transitions.Where(t => t.CurrentStateId == workorder.State.Id);

            // Return an error if the transition is not valid given the Current State
            if (await _context.Transitions.SingleAsync(t => t.CurrentStateId == workorder.State.Id && t.NextStateId == transition.Id) == null)
            {
                // TODO: Create a better error object for situations like this
                //  Perhaps include redirect url for GET Transitions
                return Conflict(new { Error = "Invalid state transition" });
            }

            workorder.State = await _context.States.FirstOrDefaultAsync(s => s.Id == transition.Id);
            _context.Entry(workorder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkorderExists(id))
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

        // ===== MATERIALS =====

        // GET: api/Workorders/5/Materials
        [HttpGet("{id}/Materials")]
        public IEnumerable<WorkorderMaterial> GetWorkorderMaterials([FromRoute] int id)
        {
            return _context.WorkorderMaterials
                .Include(m => m.Workorder.Id)
                .Include(m => m.Material)
                .Include(m => m.Vendor)
                .Where(m => m.Workorder.Id == id);
        }

        // GET: api/Workorders/5/Materials/2
        [HttpGet("{id}/Materials/{materialId}")]
        public async Task<IActionResult> GetWorkorderMaterial([FromRoute] int id, [FromRoute] int materialId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var material = await _context.WorkorderMaterials
                .Include(m => m.Workorder.Id)
                .Include(m => m.Material)
                .Include(m => m.Vendor)
                .Where(m => m.Workorder.Id == id)
                .FirstOrDefaultAsync(m => m.Id == materialId);

            if (material == null)
            {
                return NotFound();
            }

            return Ok(material);
        }
        /*
        // POST: api/Workorders/5/Materials
        [HttpPost("{id}/Materials")]
        public async Task<IActionResult> PostWorkorderMaterials([FromBody] WorkorderMaterial material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            material.Material = await 
            
            _context.WorkorderMaterials.Add(material);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkorderMaterial", new { id = material.Id }, material);
        }*/
    }
}