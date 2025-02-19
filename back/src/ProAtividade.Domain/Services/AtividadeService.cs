using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Repositories;
using ProAtividade.Domain.Interfaces.Services;

namespace ProAtividade.Domain.Services
{
    public class AtividadeService : IAtividadeService
    {
        private readonly IAtividadeRepo _atividadeRepo;

        public AtividadeService(IAtividadeRepo atividadeRepo)
        {
            _atividadeRepo = atividadeRepo;
            
        }
        public async Task<Atividade> AdicionarAtividade(Atividade model)
        {
            if(await _atividadeRepo.PegaPorTituloAsync(model.Titulo) != null)
                throw new Exception("Já existe uma atividade com esse titulo");
            if(await _atividadeRepo.PegaPorIDAsync(model.Id) == null)
            {
                _atividadeRepo.Adicionar(model);
                if(await _atividadeRepo.SalvarMudancasAsync())
                    return model;
            }

            return null;
        }

        public async Task<Atividade> AtualizarAtividade(Atividade model)
        {
            if(model.DataConclusao != null)
                throw new Exception("Não se pode alterar atividade já concluida");
            if(await _atividadeRepo.PegaPorIDAsync(model.Id) != null)
            {
                _atividadeRepo.Atualizar(model);
                if(await _atividadeRepo.SalvarMudancasAsync())
                    return model;
            }
            return null;
        }

        public async Task<bool> ConcluirAtividade(Atividade model)
        {
            if(model != null)
            {
                model.Concluir();
                _atividadeRepo.Atualizar<Atividade>(model);
                return await _atividadeRepo.SalvarMudancasAsync();
            }

            return false;
        }

        public async Task<bool> DeletarAtividade(int atividadeId)
        {
            var atividade = await _atividadeRepo.PegaPorIDAsync(atividadeId);
            if(atividade == null)
                throw new Exception("Atividade que tentou deletar não existe");

            _atividadeRepo.Deletar(atividade);
            return await _atividadeRepo.SalvarMudancasAsync();
        }

        public async Task<Atividade> PegarAtividadePorIdAsync(int atividadeId)
        {
            try
            {
                //apanhar a atividade
                var atividade = await _atividadeRepo.PegaPorIDAsync(atividadeId);
                //se nenhuma atividade tiver aquele id
                if(atividade == null) return null;
                //se alguma atividade tiver aquele id
                return atividade;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Atividade[]> PegarTodasAtividadeAsync()
        {
            try
            {
                var atividades = await _atividadeRepo.PegaTodasAsync();
                if(atividades == null) return null;

                return atividades;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}