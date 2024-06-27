using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cobit_.Data;
using Cobit_.Models;
using Cobit.Models;

namespace Cobit_.Controllers
{
    [Route("api/edm")]
    [ApiController]
    public class EDMModelsController : Controller
    {
        private readonly Cobit_Context _context;

        public EDMModelsController(Cobit_Context context)
        {
            _context = context;
        }

        // GET: api/edm
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EDMModel>>> GetEDMItems()
        {
            var edmItems = await _context.EDMItems.ToListAsync();
            return Ok(edmItems);
        }

        // POST: api/edm
        [HttpPost]
        public async Task<ActionResult<EDMModel>> PostAGItem(EDMModel model)
        {
            _context.EDMItems.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEDMItems), new { id = model.Id }, model);
        }

        // PUT: api/edm/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEDMItem(string id, EDMModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/edm/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEDMItem(string id)
        {
            var edmItem = await _context.EDMItems.FindAsync(id);
            if (edmItem == null)
            {
                return NotFound();
            }

            _context.EDMItems.Remove(edmItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}