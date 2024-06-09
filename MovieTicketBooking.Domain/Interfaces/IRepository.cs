using MovieTicketBooking.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Domain.Interfaces
{
    public interface  IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ListAllAsync(QueryOptions<T> options, bool asNoTracking=false);

        Task<int> CountAsync();

        Task<T?> GetAsync(Guid id);
        Task<T?> GetAsync(string id);
        Task<T?> GetAsync(QueryOptions<T> options, bool asNoTracking =false );

        void Add(T entity);
        void AddRange(List<T> values);

        void Remove(T entity);
        void RemoveRange(T entity);
        void RemoveRange(List<T> entities);
    }
}
