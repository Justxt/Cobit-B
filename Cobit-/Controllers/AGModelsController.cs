using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cobit.Models;
using Cobit_.Data;

namespace Cobit_.Controllers
{
    [Route("api/ag")]
    [ApiController]
    public class AGModelsController : Controller
    {
        private readonly Cobit_Context _context;

        public AGModelsController(Cobit_Context context)
        {
            _context = context;
        }

        // GET: api/ag
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AGModel>>> GetAGItems()
        {
            var agItems = await _context.AGItems.ToListAsync();
            return Ok(agItems);
        }

        // POST: api/ag
        [HttpPost]
        public async Task<ActionResult<AGModel>> PostAGItem(AGModel model)
        {
            _context.AGItems.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAGItems), new { id = model.Id }, model);
        }

        // PUT: api/ag/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAGItem(string id, AGModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/ag/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAGItem(string id)
        {
            var agItem = await _context.AGItems.FindAsync(id);
            if (agItem == null)
            {
                return NotFound();
            }

            _context.AGItems.Remove(agItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}