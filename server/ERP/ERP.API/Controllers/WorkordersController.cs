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
            return _context.Workorders
                .Include(w => w.State)
                .Include(w => w.Faculty);
        }

        // GET: api/Workorders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkorder([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders
                .Include(w => w.State)
                .Include(w => w.Estimate)
                .Include(w => w.TransitionHistory)
                    .ThenInclude(th => th.Transition)
                .Include(w => w.Attachments)
                .Include(w => w.Materials)
                .Include(w => w.TimeEntries)
                .Include(w => w.Comments)
                .Include(w => w.Notes)
                .Include(w => w.Faculty)
                .FirstOrDefaultAsync(w => w.Id == id);

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

            // TODO: Update this to ensure it pulls the first state of the active workflow
            // TODO: Update workflows/states to determine first (and last?) state of workflow
            workorder.State = await _context.States.FirstOrDefaultAsync(x => x.Id == 1);
            workorder.Faculty = await _context.Faculties.FirstOrDefaultAsync(f => f.Id == workorder.FacultyId);
            workorder.DateCreated = DateTime.Now;

            // TODO: Potentially add a default state "Created" that always has a single transition to
            //  the first state in a workflow, adding that transition to the TransitionHistory on workorder creation
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

            return NoContent();
        }

        // ===== STATE & TRANSITIONS =====

        // GET: api/Workorders/5/state
        [HttpGet("{id}/state")]
        public IActionResult GetWorkorderState([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = _context.Workorders
                .Include(w => w.State)
                .FirstOrDefault(w => w.Id == id);

            if (id != workorder.Id)
            {
                return BadRequest();
            }

            return Ok(workorder.State);
        }

        // POST: api/Workorders/5/state
        [HttpPost("{id}/state")]
        public async Task<IActionResult> UpdateWorkorderState([FromRoute] int id, [FromBody] UpdateStateDto newState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders
                .Include(w => w.State)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (id != workorder.Id || workorder == null)
            {
                return NotFound(new { Error = "Workorder " + id + " not found" });
            }
            
            // Get full state object
            var state = await _context.States.FirstOrDefaultAsync(s => s.Id == newState.NewStateId);

            if (newState.NewStateId != state.Id || state == null)
            {
                return NotFound(new { Error = "State " + newState.NewStateId + " not found" });
            }

            // TODO: Verify that it is a valid state for the active workflow

            // Get transition object inferred from current state id and the new state id
            var transition = await _context.Transitions.FirstOrDefaultAsync(t => t.CurrentStateId == workorder.State.Id && t.NextStateId == newState.NewStateId);
            
            // Verify that it is a valid transition
            if (transition == null)
            {
                // TODO: Create a better error object for situations like this
                //  Perhaps include redirect url for GET Transitions
                return Conflict(new { Error = "Invalid state transition" });
            }

            // Update state
            workorder.State = state;
            _context.Entry(workorder).State = EntityState.Modified;

            // Add transition
            var transitionHistoryEntry = new TransitionHistory
            {
                Comment = newState.Comment,
                Timestamp = DateTime.Now,
                Workorder = workorder,
                Transition = transition
            };
            _context.TransitionHistory.Add(transitionHistoryEntry);
            await _context.SaveChangesAsync();

            //return GetPossibleWorkorderTransitions(id);
            return NoContent();
        }

        // GET: api/Workorders/5/state/transitions
        [HttpGet("{id}/state/transitions")]
        public IActionResult GetPossibleWorkorderTransitions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = _context.Workorders
                .Include(w => w.State)
                .FirstOrDefault(w => w.Id == id);

            if (id != workorder.Id)
            {
                return BadRequest();
            }

            // Determine possible transitions from current state of the workorder
            var transitions = _context.Transitions
                .Include(t => t.NextState)
                .Where(t => t.CurrentStateId == workorder.State.Id)
                .OrderBy(t => t.NextStateId)
                .ToList();

            List<TransitionDto> simpleTransitions = new List<TransitionDto>();
            foreach(var t in transitions)
            {
                simpleTransitions.Add(
                    new TransitionDto {
                        Id = t.Id,
                        NextState = new StateDto {
                            Id = t.NextState.Id,
                            Name = t.NextState.Name,
                            Description = t.NextState.Description
                        }
                    });
            }

            return Ok(simpleTransitions);
        }

        // GET: api/Workorders/5/state/history
        [HttpGet("{id}/state/history")]
        public IActionResult GetWorkorderTransitionHistory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add DTO mappings so that only the state id, name, and desc. get sent
            var workorder = _context.Workorders
                .Include(w => w.TransitionHistory)
                    .ThenInclude(th => th.Transition)
                        .ThenInclude(t => t.CurrentState)
                .Include(w => w.TransitionHistory)
                    .ThenInclude(th => th.Transition)
                        .ThenInclude(t => t.NextState)
                .Single(w => w.Id == id);

            if (id != workorder.Id)
            {
                return BadRequest();
            }

            return Ok(workorder.TransitionHistory);
        }

        // ===== MATERIALS =====

        // GET: api/workorders/5/materials
        [HttpGet("{id}/Materials")]
        public async Task<IActionResult> GetWorkorderMaterials([FromRoute] int id)
        {
            if (!WorkorderExists(id))
            {
                return NotFound("Workorder with id " + id + " does not exist.");
            }

            var materials = await _context.WorkorderMaterials
                .Include(m => m.Material)
                .Include(m => m.VendorMaterial)
                    .ThenInclude(vm => vm.Vendor)
                .Include(m => m.UnitOfMeasure)
                .Where(m => m.WorkorderId == id)
                .ToListAsync();

            return Ok(materials);
        }

        // GET: api/workorders/5/materials/2
        [HttpGet("{id}/Materials/{materialId}")]
        public async Task<IActionResult> GetWorkorderMaterial([FromRoute] int id, [FromRoute] int materialId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!WorkorderExists(id))
            {
                return NotFound("Workorder with id " + id + " does not exist.");
            }

            var material = await _context.WorkorderMaterials
                .Include(m => m.Material)
                .Include(m => m.VendorMaterial)
                    .ThenInclude(vm => vm.Vendor)
                .Include(m => m.UnitOfMeasure)
                .Where(m => m.WorkorderId == id) // Are there any circumstances where this matters?
                .FirstOrDefaultAsync(m => m.Id == materialId);

            if (material == null)
            {
                return NotFound("Material with id " + material.MaterialId + " does not exist for workorder id " + id + ".");
            }

            return Ok(material);
        }

        // POST: api/workorders/5/materials
        [HttpPost("{id}/Materials")]
        public async Task<IActionResult> PostWorkorderMaterials([FromRoute] int id, [FromBody] WorkorderMaterialDto materialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders.FirstOrDefaultAsync(w => w.Id == id);
            var material = await _context.Materials.FirstOrDefaultAsync(m => m.Id == materialDto.MaterialId);

            if (workorder == null)
            {
                return NotFound("Workorder with id " + id + " does not exist.");
            }
            if (material == null)
            {
                return NotFound("Material with id " + materialDto.MaterialId + " does not exist.");
            }

            var workorderMaterial = new WorkorderMaterial {
                WorkorderId = id,
                Workorder = workorder,
                QuantityUsed = materialDto.QuantityUsed,
                MaterialId = materialDto.MaterialId,
                Material = material
            };
            
            if (materialDto.VendorId != null)
            {
                // TODO: Is it worth grabbing the whole vendor? Just do VendorExists()? Or just find VendorMaterial?
                var vendor = await _context.Vendors.FirstOrDefaultAsync(v => v.Id == materialDto.VendorId);
                var vendorMaterial = await _context.VendorMaterials
                    .FirstOrDefaultAsync(vm => vm.VendorId == materialDto.VendorId && vm.MaterialId == materialDto.MaterialId);

                // Verify vendor exists and material exists at said vendor
                if (vendor == null)
                {
                    return NotFound("Vendor with id " + materialDto.VendorId + " does not exist.");
                }
                if (vendorMaterial == null)
                {
                    return NotFound("Material with id " + materialDto.MaterialId + " does not exist at vendor id " + materialDto.VendorId + ".");
                }
                else
                {
                    workorderMaterial.VendorMaterialId = vendorMaterial.Id;
                    workorderMaterial.VendorMaterial = vendorMaterial;
                }

                // If CostPerUnit or UnitOfMeasurement are specified, set those, otherwise use VendorMaterial's
                if (materialDto.CostPerUnit == null)
                {
                    workorderMaterial.CostPerUnit = vendorMaterial.CostPerUnit;
                }
                if (materialDto.UnitOfMeasureId == null)
                {
                    workorderMaterial.UnitOfMeasureId = vendorMaterial.UnitOfMeasureId;
                    workorderMaterial.UnitOfMeasure = vendorMaterial.UnitOfMeasure;
                }
                else
                {
                    workorderMaterial.UnitOfMeasure = await _context.UnitsOfMeasure.FirstOrDefaultAsync(u => u.Id == vendorMaterial.UnitOfMeasureId);
                }
            }
            else
            {
                workorderMaterial.VendorMaterialId = null;
                workorderMaterial.VendorMaterial = null;
                workorderMaterial.CostPerUnit = materialDto.CostPerUnit;
                workorderMaterial.UnitOfMeasureId = (int)materialDto.UnitOfMeasureId;
                workorderMaterial.UnitOfMeasure = await _context.UnitsOfMeasure
                    .FirstOrDefaultAsync(u => u.Id == (int)materialDto.UnitOfMeasureId);
            }

            _context.WorkorderMaterials.Add(workorderMaterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkorderMaterial", new { id, materialId = workorderMaterial.Id }, workorderMaterial);
        }

        // ===== TIME ENTRIES =====

        // TODO: I don't think there will be any endpoints here. If there are, it'll just be redirections to PMController

        // ===== ESTIMATES =====

        // TODO: Do we need estimate endpoints? Do we need to be able to add multiple estimates?

        // ===== COMMENTS =====

        [HttpGet("{id}/comments")]
        public IActionResult GetWorkorderComments(
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var workorder = _context.Workorders
                .Include(w => w.Comments)
                .Single(w => w.Id == id);

            if (workorder == null)
            {
                return NotFound();
            }

            var comments = workorder.Comments.OrderBy(c => c.Timestamp);

            return Ok(comments);
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddNewWorkorderComment(
            [FromRoute] int id,
            [FromBody] WorkorderComment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders.FirstOrDefaultAsync(w => w.Id == id);

            if (workorder == null)
            {
                return NotFound();
            }

            comment.Timestamp = DateTime.Now;
            comment.Workorder = workorder;
            
            _context.WorkorderComments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkorderComment", new { id, commentId = comment.Id }, comment);
        }

        [HttpGet("{id}/comments/{commentId}")]
        public async Task<IActionResult> GetWorkorderComment(
            [FromRoute] int id,
            [FromRoute] int commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.WorkorderComments
                .FirstOrDefaultAsync(c => c.WorkorderId == id && c.Id == commentId);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPut("{id}/comments/{commentId}")]
        public async Task<IActionResult> UpdateWorkorderComment(
            [FromRoute] int id,
            [FromRoute] int commentId,
            [FromBody] WorkorderComment newComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(newComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkorderExists(id) || !CommentExists(commentId))
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

        [HttpDelete("{id}/comments/{commentId}")]
        public async Task<IActionResult> DeleteWorkorderComment(
            [FromRoute] int id,
            [FromRoute] int commentId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _context.WorkorderComments.SingleAsync(c => c.WorkorderId == id && c.Id == commentId);
            if (comment == null)
            {
                return NotFound();
            }

            _context.WorkorderComments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ===== NOTES =====

        [HttpGet("{id}/notes")]
        public IActionResult GetWorkorderNote(
            [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = _context.Workorders
                .Include(w => w.Notes)
                .Single(w => w.Id == id);

            if (workorder == null)
            {
                return NotFound();
            }

            var notes = workorder.Notes.OrderBy(n => n.Timestamp);

            return Ok(notes);
        }

        [HttpPost("{id}/notes")]
        public async Task<IActionResult> AddNewWorkorderNote(
            [FromRoute] int id,
            [FromBody] WorkorderNote note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workorder = await _context.Workorders.FirstOrDefaultAsync(w => w.Id == id);

            if (workorder == null)
            {
                return NotFound();
            }

            note.Timestamp = DateTime.Now;
            note.Workorder = workorder;

            _context.WorkorderNotes.Add(note);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkorderNote", new { id, noteId = note.Id }, note);
        }

        [HttpGet("{id}/notes/{noteId}")]
        public async Task<IActionResult> GetWorkorderNote(
            [FromRoute] int id,
            [FromRoute] int noteId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.WorkorderNotes
                .FirstOrDefaultAsync(c => c.WorkorderId == id && c.Id == noteId);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPut("{id}/notes/{noteId}")]
        public async Task<IActionResult> UpdateWorkorderNote(
            [FromRoute] int id,
            [FromRoute] int noteId,
            [FromBody] WorkorderNote newNote)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (noteId != newNote.Id)
            {
                return BadRequest();
            }

            _context.Entry(newNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkorderExists(id) || !NoteExists(noteId))
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

        [HttpDelete("{id}/notes/{noteId}")]
        public async Task<IActionResult> DeleteWorkorderNote(
            [FromRoute] int id,
            [FromRoute] int noteId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _context.WorkorderNotes.SingleAsync(n => n.WorkorderId == id && n.Id == noteId);
            if (note == null)
            {
                return NotFound();
            }

            _context.WorkorderNotes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ===== ATTACHMENTS =====

        // ===== FACULTIES =====

        [HttpGet("faculties")]
        public IEnumerable<Faculty> GetFaculties()
        {
            return _context.Faculties;
        }

        // ===== USES =====

        [HttpGet("uses")]
        public IEnumerable<Use> GetUses()
        {
            return _context.Uses;
        }

        // ===== HELPERS =====

        private bool WorkorderExists(int id)
        {
            return _context.Workorders.Any(e => e.Id == id);
        }

        private bool CommentExists(int id)
        {
            return _context.WorkorderComments.Any(c => c.Id == id);
        }

        private bool NoteExists(int id)
        {
            return _context.WorkorderNotes.Any(c => c.Id == id);
        }
    }
}