using Microsoft.AspNetCore.Mvc;
using WebApi.Context;
using WebApi.Entities;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController :ControllerBase
    {

        private readonly AgendaContext _context;
        public ContatoController(AgendaContext context) {
            _context = context;


         }
        
        [HttpPost]
        public IActionResult Create(Contato contato)
        {

            _context.Add(contato);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = contato.Id},contato);

        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id) {
        var contato = _context.Contatos.Find(id);
            if (contato == null)
            {
                return NotFound();
            }

        return Ok(contato);
        }
        
        [HttpGet("name")]
        public IActionResult GetName(string name) {

            var ContatoBanco = _context.Contatos.Where(x => x.Nome.Contains(name));
            return Ok(ContatoBanco);

        
        }


        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Contato contato)
        {
            var ContatoBanco = _context.Contatos.Find(id);

            if (ContatoBanco == null)
            {
                return NotFound();
                
            }
            ContatoBanco.Nome = contato.Nome;
            ContatoBanco.Ativo  = contato.Ativo;
            ContatoBanco.Telefone = contato.Telefone;

            _context.Contatos.Update(ContatoBanco);
            _context.SaveChanges();
            return Ok(ContatoBanco);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            var contato = _context.Contatos.Find(id);
            _context.Contatos.Remove(contato);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
