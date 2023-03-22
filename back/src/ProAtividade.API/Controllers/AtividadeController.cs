using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAtividade.API.Models;

namespace ProAtividade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadeController : ControllerBase
    {
        public IEnumerable<Atividade> Atividades = new List<Atividade>(){
            new Atividade(1),
            new Atividade(2),
            new Atividade(3),
        };

        [HttpGet]
        public IEnumerable<Atividade> get()
        {
            return Atividades;
        }

        [HttpGet("{id}")]
        public Atividade get(int id)
        {
            return Atividades.FirstOrDefault(ati => ati.Id == id);
        }

        [HttpPost]
        public IEnumerable< Atividade> post(Atividade atividade)
        {
            return Atividades.Append<Atividade>(atividade);
        }

        [HttpPut("{id}")]
        public Atividade put(int id, Atividade atividade)
        {
            atividade.Id = atividade.Id + 1;
            return atividade;
        }

        [HttpDelete("{id}")]
        public string delete(int id)
        {
            return "Meu primeiro delete";
        }
    }
}