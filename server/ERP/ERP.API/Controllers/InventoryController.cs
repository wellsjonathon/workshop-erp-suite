using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Models.Inventory;
using ERP.Repositories.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===== HELPERS =====
        private bool MaterialExists(int id)
        {
            return _context.Materials.Any(m => m.Id == id);
        }

        // ===== MATERIALS =====

        // GET: api/inventory/materials
        [HttpGet("materials")]
        public IEnumerable<Material> GetMaterials()
        {
            return _context.Materials;
        }
        // POST: api/inventory/materials
        [HttpPost("materials")]
        public async Task<IActionResult> CreateNewMaterial([FromBody] Material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterial", new { id = material.Id }, material)
        }
        // GET: api/inventory/materials/{id}
        [HttpGet("materials/{id}")]
        public async Task<IActionResult> GetMaterial([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);

            if (material == null)
            {
                return NotFound();
            }

            return Ok(material);
        }
        // PUT: api/inventory/materials/{id}
        [HttpPut("materials/{id}")]
        public async Task<IActionResult> UpdateMaterial([FromRoute] int id, [FromBody] Material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(material).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(id))
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
        // DELETE: api/inventory/materials/{id}
        [HttpDelete("materials/{id}")]

        // ===== MATERIAL TYPES =====

        // GET: api/inventory/materials/types
        [HttpGet("materials/types")]
        // POST: api/inventory/materials/types
        [HttpPost("materials/types")]
        // GET: api/inventory/materials/types/{id}
        [HttpGet("materials/types/{id}")]
        // PUT: api/inventory/materials/types/{id}
        [HttpPut("materials/types/{id}")]
        // DELETE: api/inventory/materials/types/{id}
        [HttpDelete("materials/types/{id}")]

        // ===== MATERIAL CATEGORIES =====

        // GET: api/inventory/materials/categories
        [HttpGet("materials/categories")]
        // POST: api/inventory/materials/categories
        [HttpPost("materials/categories")]
        // GET: api/inventory/materials/categories/{id}
        [HttpGet("materials/categories/{id}")]
        // PUT: api/inventory/materials/categories/{id}
        [HttpPut("materials/categories/{id}")]
        // DELETE: api/inventory/materials/categories/{id}
        [HttpDelete("materials/categories/{id}")]

        // ===== VENDORS =====

        // GET: api/inventory/vendors
        [HttpGet("materials/vendors")]
        // POST: api/inventory/vendors
        [HttpPost("materials/vendors")]
        // GET: api/inventory/vendors/{id}
        [HttpGet("materials/vendors/{id]")]
        // PUT: api/inventory/vendors/{id}
        [HttpPut("materials/vendors/{id}")]
        // DELETE: api/inventory/vendors/{id}
        [HttpDelete("materials/vendors/{id}")]

        // ===== ORDERS =====

        // GET: api/inventory/orders
        [HttpGet("materials/orders")]
        // POST: api/inventory/orders
        [HttpPost("materials/orders")]
        // GET: api/inventory/orders/{id}
        [HttpGet("materials/orders/{id}")]
        // PUT: api/inventory/orders/{id}
        [HttpPut("materials/orders/{id}")]
        // DELETE: api/inventory/orders/{id}
        [HttpDelete("materials/orders/{id}")]

        // ===== ORDER ITEMS =====

        // GET: api/inventory/orders/{id}/items
        [HttpGet("materials/orders/{id}/items")]
        // POST: api/inventory/orders/{id}/items
        [HttpPost("materials/orders/{id}/items")]
        // GET: api/inventory/orders/{id}/items/{itemId}
        [HttpGet("materials/orders/{id}/items/{itemId}")]
        // PUT: api/inventory/orders/{id}/items/{itemId}
        [HttpPut("materials/orders/{id}/items/{itemId}")]
        // DELETE: api/inventory/orders/{id}/items/{itemId}
        [HttpDelete("materials/orders/{id}/items/{itemId}")]
    }
}