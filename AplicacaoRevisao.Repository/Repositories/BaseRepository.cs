using AplicacaoRevisao.Domain.Interfaces;
using AplicacaoRevisao.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AplicacaoRevisao.Repository.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        internal readonly AplicacaoRevisaoContext _context;

        protected BaseRepository(AplicacaoRevisaoContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T item)
        {
            _context.Set<T>().AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).CountAsync();
        }

        public async Task<int> CountAsync<K>(Expression<Func<T, IEnumerable<K>>> selectExpression)
        {
            return await _context.Set<T>().Select(selectExpression).Distinct().CountAsync();
        }

        public async Task<int> CountAsync<K>(Expression<Func<T, bool>> expression, Expression<Func<T, IEnumerable<K>>> selectExpression)
        {
            return await _context.Set<T>().Where(expression).Select(selectExpression).Distinct().CountAsync();
        }

        public async Task<T> EditAsync(T item)
        {
            _context.Set<T>().Update(item);
            await _context.SaveChangesAsync();

            return item; 
        }

        public async Task<T> FindAsNoTrackingAsync(Expression<Func<T, bool>> expression) 
        {
            // serve apenas p/ consulta quando não se pretende modificar o obj no ET
            // busca de melhor performance caso não seja necessário atualizar informações na base de dados
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> FindAsync(decimal id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<List<T>> ListPaginationAsync<K>(Expression<Func<T, K>> sortExpression, int pagina, int quantidade)
        {
            return await _context.Set<T>().OrderBy(sortExpression).Skip(quantidade * (pagina - 1)).Take(quantidade).ToListAsync();
        }

        public async Task<List<T>> ListPaginationAsync<K>(Expression<Func<T, bool>> expression, Expression<Func<T, K>> sortExpression, int pagina, int quantidade)
        {
            return await _context.Set<T>().Where(expression).OrderBy(sortExpression).Skip(quantidade * (pagina - 1)).Take(quantidade).ToListAsync();
        }

        public async Task RemoveAsync(T item)
        {
            _context.Set<T>().Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
