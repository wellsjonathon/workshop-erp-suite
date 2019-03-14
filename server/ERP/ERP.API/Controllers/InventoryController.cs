using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Models;
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
        private bool MaterialTypeExists(int id)
        {
            return _context.MaterialTypes.Any(t => t.Id == id);
        }
        private bool MaterialCategoryExists(int id)
        {
            return _context.MaterialCategories.Any(c => c.Id == id);
        }
        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(v => v.Id == id);
        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(o => o.Id == id);
        }
        private bool OrderItemExists(int id)
        {
            return _context.OrderItems.Any(i => i.Id == id);
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
        public async Task<IActionResult> CreateMaterial([FromBody] Material material)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            material.Category = await _context.MaterialCategories.FirstOrDefaultAsync(c => c.Id == material.CategoryId);
            material.Type = await _context.MaterialTypes.FirstOrDefaultAsync(c => c.Id == material.TypeId);
            material.UnitOfMeasure = await _context.UnitsOfMeasure.FirstOrDefaultAsync(u => u.Id == material.UnitOfMeasureId);

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterial", new { id = material.Id }, material);
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
        public async Task<IActionResult> DeleteMaterial([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var material= await _context.Materials.FirstOrDefaultAsync(m => m.Id == id);
            if (material == null)
            {
                return NotFound();
            }

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // ===== MATERIAL TYPES =====

        // GET: api/inventory/materials/types
        [HttpGet("materials/types")]
        public IEnumerable<MaterialType> GetMaterialTypes()
        {
            return _context.MaterialTypes;
        }
        // POST: api/inventory/materials/types
        [HttpPost("materials/types")]
        public async Task<IActionResult> CreateMaterialType([FromBody] MaterialType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MaterialTypes.Add(type);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterialType", new { id = type.Id }, type);
        }
        // GET: api/inventory/materials/types/{id}
        [HttpGet("materials/types/{id}")]
        public async Task<IActionResult> GetMaterialType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var type = await _context.MaterialTypes.FirstOrDefaultAsync(t => t.Id == id);

            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }
        // PUT: api/inventory/materials/types/{id}
        [HttpPut("materials/types/{id}")]
        public async Task<IActionResult> UpdateMaterialType([FromRoute] int id, [FromBody] MaterialType type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(type).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialTypeExists(id))
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
        // DELETE: api/inventory/materials/types/{id}
        [HttpDelete("materials/types/{id}")]
        public async Task<IActionResult> DeleteMaterialType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var type = await _context.MaterialTypes.FirstOrDefaultAsync(t => t.Id == id);
            if (type == null)
            {
                return NotFound();
            }

            _context.MaterialTypes.Remove(type);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // ===== MATERIAL CATEGORIES =====

        // GET: api/inventory/materials/categories
        [HttpGet("materials/categories")]
        public IEnumerable<MaterialCategory> GetMaterialCategories()
        {
            return _context.MaterialCategories;
        }
        // POST: api/inventory/materials/categories
        [HttpPost("materials/categories")]
        public async Task<IActionResult> CreateMaterialCategory([FromBody] MaterialCategory category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MaterialCategories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterialCategory", new { id = category.Id }, category);
        }
        // GET: api/inventory/materials/categories/{id}
        [HttpGet("materials/categories/{id}")]
        public async Task<IActionResult> GetMaterialCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.MaterialCategories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }
        // PUT: api/inventory/materials/categories/{id}
        [HttpPut("materials/categories/{id}")]
        public async Task<IActionResult> UpdateMaterialCategory([FromRoute] int id, [FromBody] MaterialCategory category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialCategoryExists(id))
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
        // DELETE: api/inventory/materials/categories/{id}
        [HttpDelete("materials/categories/{id}")]
        public async Task<IActionResult> DeleteMaterialCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.MaterialCategories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.MaterialCategories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // ===== VENDORS =====

        // GET: api/inventory/vendors
        [HttpGet("vendors")]
        public IEnumerable<Vendor> GetVendors()
        {
            return _context.Vendors;
        }
        // POST: api/inventory/vendors
        [HttpPost("vendors")]
        public async Task<IActionResult> CreateVendor([FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
        }
        // GET: api/inventory/vendors/{id}
        [HttpGet("vendors/{id}")]
        public async Task<IActionResult> GetVendor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);

            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }
        // PUT: api/inventory/vendors/{id}
        [HttpPut("vendors/{id}")]
        public async Task<IActionResult> UpdateVendor([FromRoute] int id, [FromBody] Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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
        // DELETE: api/inventory/vendors/{id}
        [HttpDelete("vendors/{id}")]
        public async Task<IActionResult> DeleteVendor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        // ===== VENDOR MATERIALS =====

        // GET: api/inventory/vendors/{id}/materials
        [HttpGet("vendors/{id}/materials")]
        public IEnumerable<VendorMaterial> GetMaterialsFromVendor([FromRoute] int id)
        {
            return _context.VendorMaterials
                .Include(vm => vm.Material)
                .Where(vm => vm.VendorId == id);
        }
        // POST: api/inventory/vendors/{id}/materials
        [HttpPost("vendors/{id}/materials")]
        public async Task<IActionResult> AddMaterialToVendor([FromRoute] int id, [FromBody] VendorMaterialDto vendorMaterialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate vendor exists
            var vendor = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == id);
            if (vendor == null)
            {
                return NotFound("Vendor " + id + " does not exist.");
            }

            // Validate material exists
            var material = await _context.Materials
                .Include(m => m.UnitOfMeasure)
                .FirstOrDefaultAsync(m => m.Id == vendorMaterialDto.MaterialId);
            if (material == null)
            {
                return NotFound("Material " + vendorMaterialDto.MaterialId + " does not exist.");
            }

            var vendorMaterial = new VendorMaterial
            {
                DateLastPurchased = null,
                CostPerUnit = vendorMaterialDto.CostPerUnit,
                VendorId = id,
                Vendor = vendor,
                MaterialId = vendorMaterialDto.MaterialId,
                Material = material
            };

            // If UnitOfMeasureId isn't specified, utilize default from material, otherwise add specified
            if (vendorMaterialDto.UnitOfMeasureId == null)
            {
                vendorMaterial.UnitOfMeasureId = material.UnitOfMeasureId;
                vendorMaterial.UnitOfMeasure = material.UnitOfMeasure;
            }
            else
            {
                vendorMaterial.UnitOfMeasureId = (int)vendorMaterialDto.UnitOfMeasureId;
                vendorMaterial.UnitOfMeasure = await _context.UnitsOfMeasure
                    .FirstOrDefaultAsync(u => u.Id == vendorMaterialDto.UnitOfMeasureId);
            }

            // Add relation
            _context.VendorMaterials.Add(vendorMaterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaterialFromVendor", new { id = vendorMaterial.VendorId, materialId = vendorMaterial.MaterialId }, vendorMaterial);
        }
        // GET: api/inventory/vendors/{id}/materials/{materialId}
        [HttpGet("vendors/{id}/materials/{materialId}")]
        public async Task<IActionResult> GetMaterialFromVendor([FromRoute] int id, [FromRoute] int materialId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate vendor-material relation exists
            var vendorMaterial = await _context.VendorMaterials
                .Include(vm => vm.Vendor)
                .Include(vm => vm.Material)
                    .ThenInclude(m => m.UnitOfMeasure)
                .Include(vm => vm.Material)
                    .ThenInclude(m => m.Type)
                .Include(vm => vm.Material)
                    .ThenInclude(m => m.Category)
                .FirstOrDefaultAsync(vm => vm.VendorId == id && vm.MaterialId == materialId);

            if (vendorMaterial == null)
            {
                return NotFound("Material " + materialId + " does not exist with vendor " + id + ".");
            }

            return Ok(vendorMaterial);
        }
        // TODO: Add PUT and DELETE endpoints

        // ===== ORDERS =====

        // GET: api/inventory/orders
        [HttpGet("orders")]
        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders;
        }
        // POST: api/inventory/orders
        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }
        // GET: api/inventory/orders/{id}
        [HttpGet("orders/{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
        // PUT: api/inventory/orders/{id}
        [HttpPut("orders/{id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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
        // DELETE: api/inventory/orders/{id}
        [HttpDelete("orders/{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // ===== ORDER ITEMS =====

        // GET: api/inventory/orders/{id}/items
        [HttpGet("orders/{id}/items")]
        public IEnumerable<OrderItem> GetOrderItems([FromRoute] int id)
        {
            return _context.Orders
                .FirstOrDefault(o => o.Id == id)
                .Items;
        }
        // POST: api/inventory/orders/{id}/items
        [HttpPost("orders/{id}/items")]
        public async Task<IActionResult> CreateOrderItem([FromRoute] int id, [FromBody] OrderItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            item.OrderId = id;

            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderItem", new { id = item.Id }, item);
        }
        // GET: api/inventory/orders/{id}/items/{itemId}
        [HttpGet("orders/{id}/items/{itemId}")]
        public async Task<IActionResult> GetOrderItem([FromRoute] int id, [FromRoute] int itemId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == itemId && i.OrderId == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
        // PUT: api/inventory/orders/{id}/items/{itemId}
        [HttpPut("orders/{id}/items/{itemId}")]
        public async Task<IActionResult> UpdateOrderItem([FromRoute] int id, [FromRoute] int itemId, [FromBody] OrderItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderItemExists(id))
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
        // DELETE: api/inventory/orders/{id}/items/{itemId}
        [HttpDelete("orders/{id}/items/{itemId}")]
        public async Task<IActionResult> DeleteOrderItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // ===== UNITS OF MEASURE =====

        // GET: api/inventory/units-of-measure
        [HttpGet("units-of-measure")]
        public IEnumerable<UnitOfMeasure> GetUnitsOfMeasure()
        {
            return _context.UnitsOfMeasure;
        }
    }
}