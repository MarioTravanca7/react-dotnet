using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAtividade.API.Data;
using ProAtividade.API.Models;

namespace ProAtividade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadeController : ControllerBase
    {
        private readonly DataContext _context;
        public AtividadeController(DataContext context)
        {
            _context = context;

        }

        [HttpGet]
        public IEnumerable<Atividade> get()
        {
            return _context.Atividades;
        }

        [HttpGet("{id}")]
        public Atividade get(int id)
        {
            return _context.Atividades.FirstOrDefault(ati => ati.Id == id);
        }

        [HttpPost]
        public Atividade post(Atividade atividade)
        {
            _context.Atividades.Add(atividade);
            if (_context.SaveChanges() > 0)
                return _context.Atividades.FirstOrDefault(ati => ati.Id == atividade.Id);
            else
                throw new Exception("Nao conseguiste add uma atividade");
        }

        [HttpPut("{id}")]
        public Atividade put(int id, Atividade atividade)
        {
            if (atividade.Id != id) throw new Exception("Estas a tentar atualizar a atividade errada");

            _context.Update(atividade);
            if (_context.SaveChanges() > 0)
                return _context.Atividades.FirstOrDefault(ativ => ativ.Id == id);
            else
                return new Atividade();
        }

        [HttpDelete("{id}")]
        public bool delete(int id)
        {
            var atividade = _context.Atividades.FirstOrDefault(ativ => ativ.Id == id);
            if(atividade == null)
                throw new Exception("Estas a tentar apagar uma atividade que nao existe");
            
            _context.Remove(atividade);
            return _context.SaveChanges() > 0;
        }
    }
}