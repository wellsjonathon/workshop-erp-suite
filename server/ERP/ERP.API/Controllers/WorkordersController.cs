﻿using System;
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
            return _context.Workorders;
        }

        // GET: api/Workorders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkorder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders.FindAsync(id); //get.include(context something)

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

        // POST: api/Workorders
        [HttpPost]
        public async Task<IActionResult> PostWorkorder([FromBody] Workorder workorder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            workorder.Status = _context.WorkorderStatuses.Single(x => x.ID == 1);
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
    }
}