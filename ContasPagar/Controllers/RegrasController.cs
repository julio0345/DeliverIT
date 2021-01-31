using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContasPagar.InfraStructure;
using ContasPagar.Models;
using ContasPagar.ViewModels;

namespace ContasPagar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegrasController : ControllerBase
    {
        private readonly ContasContext _context;

        public RegrasController(ContasContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Regra>>> GetRegras()
        {
            return await _context.Regras.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Regra>> GetRegra(Guid id)
        {
            var regra = await _context.Regras.FindAsync(id);

            if (regra == null)
            {
                return NotFound();
            }

            return regra;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegra(Guid id, Regra regra)
        {
            if (id != regra.Id)
            {
                return BadRequest();
            }

            _context.Entry(regra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegraExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Regra>> PostRegra(RegraPost regraPost)
        {
            var regra = regraPost.ToModel();

            _context.Regras.Add(regra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegra", new { id = regra.Id }, regra);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Regra>> DeleteRegra(Guid id)
        {
            var regra = await _context.Regras.FindAsync(id);
            if (regra == null)
            {
                return NotFound();
            }

            _context.Regras.Remove(regra);
            await _context.SaveChangesAsync();

            return regra;
        }

        private bool RegraExists(Guid id)
        {
            return _context.Regras.Any(e => e.Id == id);
        }
    }
}