using ContasPagar.InfraStructure;
using ContasPagar.Models;
using ContasPagar.Services.Interfaces;
using ContasPagar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContasPagar.Controllers
{
    /// <summary>
    /// Api de contas a pagar versão 1.0
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContasController : ControllerBase
    {
        private readonly ContasContext _context;
        private readonly IContasService _contasService;

        public ContasController(ContasContext context, IContasService contasService)
        {
            _context = context;
            _contasService = contasService;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ContaPagarList>>> GetContasPagar()
        {
            var contas = await _contasService.RetornarListaContas();
            return Ok(contas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContaPagar>> GetContaPagar(Guid id)
        {
            var contaAPagar = await _context.ContaPagar.FindAsync(id);

            if (contaAPagar == null)
            {
                return NotFound();
            }

            return contaAPagar;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutContaPagar(Guid id, ContaPagar contaAPagar)
        {
            if (id != contaAPagar.Id)
            {
                return BadRequest();
            }

            _context.Entry(contaAPagar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContaAPagarExists(id))
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

        [HttpPost("")]
        public async Task<ActionResult<ContaPagar>> PostContaPagar(ContaPagarPost contaAPagar)
        {
            var conta = await _contasService.AplicarRegras(contaAPagar);

            _context.ContaPagar.Add(conta);
            await _context.SaveChangesAsync();

            //Padrão Rest
            return CreatedAtAction("GetContaPagar", new { id = conta.Id }, conta);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ContaPagar>> DeleteContaPagar(Guid id)
        {
            var contaAPagar = await _context.ContaPagar.FindAsync(id);
            if (contaAPagar == null)
            {
                return NotFound();
            }

            _context.ContaPagar.Remove(contaAPagar);
            await _context.SaveChangesAsync();

            //Padrão Rest
            return contaAPagar;
        }

        private bool ContaAPagarExists(Guid id)
        {
            return _context.ContaPagar.Any(e => e.Id == id);
        }
    }
}