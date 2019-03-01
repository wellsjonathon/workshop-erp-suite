using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models.ProjectManagement;
using ERP.Repositories.Context;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERP.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class PMController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PMController(ApplicationDbContext context)
        {
            _context = context;
        }

        private bool TimeEntryExists(int id)
        {
            return _context.TimeEntries.Any(t => t.Id == id);
        }

        // ===== Time Entries ===== 
        // GET: api/calendar/time-entries
        [HttpGet("calendar/time-entries")]
        public IEnumerable<TimeEntry> GetTimeEntries()
        {
            return _context.TimeEntries;
        }

        // POST: api/calendar/time-entries
        [HttpPost("calendar/time-entries")]
        public async Task<IActionResult> PostTimeEntry([FromBody] TimeEntry timeEntry)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeEntries", new { id = timeEntry.Id}, timeEntry);
        }

        // GET: api/calendar/time-entries/{time-entry-id}
        [HttpGet("calendar/time-entries/{id}")]
        public async Task<IActionResult> GetTimeEntry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var time_entry = await _context.TimeEntries.FirstOrDefaultAsync(t => t.Id == id);

            if (time_entry == null)
            {
                return NotFound();
            }
            return Ok(time_entry);
        }

        // PUT: api/calendar/time-entries/{time-entry-id}
        [HttpPut("calendar/time-entries/{id}")]
        public async Task<IActionResult> PutTimeEntry([FromRoute] int id, [FromBody] TimeEntry timeEntry)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(id != timeEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(timeEntry).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }

            catch
            {
                if (!TimeEntryExists(id))
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

        //DELETE: api/calendar/time-entries/{time-entry-id}
        [HttpDelete("calendar/time-entries/{id}")]
        public async Task<IActionResult> DeleteTimeEntry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var time_entry = await _context.TimeEntries.FindAsync(id);
            if(time_entry == null)
            {
                return NotFound();
            }

            _context.TimeEntries.Remove(time_entry);
            await _context.SaveChangesAsync();

            return Ok(time_entry);
        }
    }
}
