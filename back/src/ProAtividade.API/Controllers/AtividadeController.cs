using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProAtividade.Data.Context;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Services;

namespace ProAtividade.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadeController : ControllerBase
    {
        private readonly IAtividadeService _atividadeService;

        public AtividadeController(IAtividadeService atividadeService)
        {
            _atividadeService = atividadeService;
        }

        [HttpGet]
        public async Task<IActionResult> get()
        {
            try
            {
                var atividades = await _atividadeService.PegarTodasAtividadeAsync();
                if(atividades == null) return NoContent();

                return Ok(atividades);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar Atividades. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>  get(int id)
        {
            try
            {
                var atividade = await _atividadeService.PegarAtividadePorIdAsync(id);
                if(atividade == null) return NoContent();

                return Ok(atividade);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar recuperar Atividade com id ${id}. Erro: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> post(Atividade model)
        {
            try
            {
                var atividade = await _atividadeService.AdicionarAtividade(model);
                if(atividade == null) return NoContent();

                return Ok(atividade);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar adicionar Atividade. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> put(int id, Atividade model)
        {
            try
            {
                if(model.Id != id) 
                    this.StatusCode(StatusCodes.Status409Conflict,
                        "Você está tentando atualizar a atividade errada");
                
                var atividade = await _atividadeService.AtualizarAtividade(model);
                if(atividade == null) return NoContent();

                return Ok(atividade);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar Atualizar Atividade com id ${id}. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id)
        {
            try
            {
                var atividade = await _atividadeService.PegarAtividadePorIdAsync(id);
                if(atividade == null) 
                    this.StatusCode(StatusCodes.Status409Conflict,
                        "Você está tentando eliminar a atividade que nao existe");
                
                if(await _atividadeService.DeletarAtividade(id))
                {
                    return Ok(new { message = "Deletado" });
                }
                else{
                    return BadRequest("Ocorreu um problema nao especifico ao tentar eliminar a atividade");

                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao tentar eliminar Atividade com id ${id}. Erro: {ex.Message}");
            }
        }
    }
}