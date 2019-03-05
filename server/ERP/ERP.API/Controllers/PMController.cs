using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Models;
using ERP.Models.Workorders;
using ERP.Models.Project_Management;
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

        //Check if entries exist
        private bool TimeEntryExists(int id)
        {
            return _context.TimeEntries.Any(t => t.ID == id);
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.ID == id);
        }

        private bool AvailabilityExists(int id)
        {
            return _context.Availabilities.Any(a => a.ID == id);
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

            timeEntry.TimeType =  _context.TimeTypes.Single<TimeType>(t => t.ID == timeEntry.TimeType.ID);
            timeEntry.DateTime = DateTime.Now;
            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTimeEntries", new { id = timeEntry.ID}, timeEntry);
        }

        // GET: api/calendar/time-entries/{time-entry-id}
        [HttpGet("calendar/time-entries/{id}")]
        public async Task<IActionResult> GetTimeEntry([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var time_entry = await _context.TimeEntries.FirstOrDefaultAsync(t => t.ID == id);

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
            if(id != timeEntry.ID)
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

        //===== Events ===== 
        //GET: api/calendar/events/
        [HttpGet("calendar/events")]
        public IEnumerable<Event> GetEvents()
        {
            return _context.Events;
        }

        //POST: api/calendar/events/
        [HttpPost("calendar/events")]
        public async Task<IActionResult> PostEvent([FromBody] Event event_entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            event_entry.Start = DateTime.Now;
            _context.Events.Add(event_entry);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetEvent", new { id = event_entry.ID }, event_entry);
        }

        //GET: api/calendar/events/id
        [HttpGet("calendar/events/{id}")]
        public async Task<IActionResult> GetEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var event_entry = await _context.Events.FirstOrDefaultAsync(e => e.ID == id);
            if (event_entry == null)
            {
                return NotFound();
            }
            return Ok(event_entry);
        }

        //PUT: api/calendar/events/id
        [HttpPut("calendar/events/{id}")]
        public async Task<IActionResult> PutEvent([FromRoute] int id, [FromBody] Event event_entry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(id != event_entry.ID)
            {
                return BadRequest();
            }
            _context.Entry(event_entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // DELETE: api/calendar/events/id
        [HttpDelete("calendar/events/{id}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var event_entry = await _context.Events.FindAsync();
            await _context.SaveChangesAsync();
            return Ok(event_entry);
        }

        //===== Availability =====
        // GET: api/calendar/availability
        [HttpGet("calendar/availability")]
        public IEnumerable<Availability> GetAvailabilities()
        {
            return _context.Availabilities;
        }

        // POST: api/calendar/availability
        [HttpPost("calendar/availability")]
        public async Task<IActionResult> PostAvailability([FromBody] Availability availability)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            availability.AvailabilityTypeId = _context.AvailabilityTypes.Single<AvailabilityType>(a => a.ID == availability.AvailabilityTypeId.ID);
            _context.Availabilities.Add(availability);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetAvailability", new { id = availability.ID }, availability);
        }
    }
}
