using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAtividade.Data.Context;
using ProAtividade.Domain.Entities;
using ProAtividade.Domain.Interfaces.Repositories;

namespace ProAtividade.Data.Repositories
{
    public class AtividadeRepo : GeralRepo, IAtividadeRepo
    {
        private readonly DataContext _context;

        public AtividadeRepo(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Atividade> PegaPorIDAsync(int id)
        {
            IQueryable<Atividade> query = _context.Atividades;

            // orderby(neste caso) -> apanha os elementos da base de dados e ordena por ordem do ID
            //where -> apanha o elemento da BD que tem o mesmo id passado 
            query = query.AsNoTracking().OrderBy(ativ => ativ.Id).Where(a => a.Id == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Atividade> PegaPorTituloAsync(string titulo)
        {
            IQueryable<Atividade> query = _context.Atividades;
            query = query.AsNoTracking()
                .OrderBy(ativ => ativ.Id)
                .Where(a => a.Titulo == titulo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Atividade[]> PegaTodasAsync()
        {
            IQueryable<Atividade> query = _context.Atividades;
            query = query.AsNoTracking()
                .OrderBy(ativ => ativ.Id);

            return await query.ToArrayAsync();
        }
    }
}