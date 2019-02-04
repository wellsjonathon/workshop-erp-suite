using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Repositories.Context;

// TODO: Delegate any calls that needs to build the object to the
//         services - allows use of multiple contexts

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkordersController : ControllerBase
    {
        private readonly WorkorderContext _context;

        public WorkordersController(WorkorderContext context)
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
            //    w.Status = _context.WorkorderStatuses.FirstOrDefaultAsync(s => s.ID == w.Status.ID);
            //}
            return _context.Workorders.Include(w => w.Status);
        }

        // GET: api/Workorders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkorder([FromRoute] int id)
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

            if (id != workorder.ID)
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

        // PUT: api/Workorders/5/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutWorkorderStatus([FromRoute] int id, [FromBody] WorkorderStatus status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders.FirstOrDefaultAsync(w => w.ID == id);

            if (id != workorder.ID)
            {
                return BadRequest();
            }

            workorder.Status = await _context.WorkorderStatuses.FirstOrDefaultAsync(s => s.ID == status.ID);
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

            workorder.Status = await _context.WorkorderStatuses.FirstOrDefaultAsync(x => x.ID == 1);
            workorder.DateCreated = DateTime.Now;

            _context.Workorders.Add(workorder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkorder", new { id = workorder.ID }, workorder);
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
            return _context.Workorders.Any(e => e.ID == id);
        }

        // Workorder Materials

        // GET: api/Workorders/5/Materials
        [HttpGet("{id}/Materials")]
        public IEnumerable<WorkorderMaterial> GetWorkorderMaterials([FromRoute] int id)
        {
            return _context.WorkorderMaterials
                .Include(m => m.Workorder.ID)
                .Include(m => m.Material)
                .Include(m => m.Vendor)
                .Where(m => m.Workorder.ID == id);
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
                .Include(m => m.Workorder.ID)
                .Include(m => m.Material)
                .Include(m => m.Vendor)
                .Where(m => m.Workorder.ID == id)
                .FirstOrDefaultAsync(m => m.ID == materialId);

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

            return CreatedAtAction("GetWorkorderMaterial", new { id = material.ID }, material);
        }*/
    }
}