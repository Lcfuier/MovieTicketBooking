using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Domain.Interfaces;
using MovieTicketBooking.Domain.Queries;
using MovieTicketBooking.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // private backing field bc when filtering (where) count might be less than _dbset.Count()
        private int? count;

        protected readonly MovieTicketBookingDbcontext _context;
        private readonly DbSet<T> _dbset;

        public Repository(MovieTicketBookingDbcontext context)
        {
            _context = context;
            _dbset = _context.Set<T>(); // _context.Books()
        }

        public async Task<IEnumerable<T>> ListAllAsync(QueryOptions<T> options, bool asNoTracking = false) =>
            await BuildQuery(options, asNoTracking).ToListAsync();

        // if count is null (where is not use) then use _dbset.CountAsync()
        public async Task<int> CountAsync() => count ?? await _dbset.CountAsync();

        public virtual async Task<T?> GetAsync(Guid id) =>
            await _dbset.FindAsync(id);
        public virtual async Task<T?> GetAsync(string id, bool asNoTracking = false) =>
            await _dbset.FindAsync(id);
        public virtual async Task<T?> GetAsync(QueryOptions<T> options, bool asNoTracking = false) =>
            await BuildQuery(options, asNoTracking).FirstOrDefaultAsync();

        private IQueryable<T> BuildQuery(QueryOptions<T> options, bool asNoTracking)
        {
            IQueryable<T> query = _dbset; // ex: _context.Books;

            if (options.HasInclude)
            {
                foreach (string include in options.GetIncludes())
                {
                    query = query.Include(include);
                }
            }

            if (options.HasWhere)
            {
                foreach (Expression<Func<T, bool>> expression in options.WhereClauses)
                {
                    query = query.Where(expression);
                }
                count = query.Count(); // get filter count
            }

            if (options.HasOrderBy)
            {
                if (options.OrderByDirection == "asc")
                {
                    query = query.OrderBy(options.OrderBy);
                }
                else
                {
                    query = query.OrderByDescending(options.OrderBy);
                }
            }

            if (options.HasPaging)
            {
                query = query.Skip(options.PageSize * (options.PageNumber - 1))
                .Take(options.PageSize);
            }
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public void Add(T entity) => _dbset.Add(entity);
        public void AddRange(List<T> values) => _dbset.AddRange(values);

        public void Remove(T entity) => _dbset.Remove(entity);
        public void RemoveRange(T entity) => _dbset.RemoveRange(entity);
        public void RemoveRange(List<T> entities) => _dbset.RemoveRange(entities);
    }
}
