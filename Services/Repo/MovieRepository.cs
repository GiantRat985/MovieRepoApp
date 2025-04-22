using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MovieRepoApp.Models;
using MovieRepoApp.Models.Tables;
using SQLite;

namespace MovieRepoApp.Services.Repo
{
    public class MovieRepository<T> : IRepository<T> where T : class
    {
        private SQLiteAsyncConnection _connection;
        private readonly ISQLiteConnectionFactory _connectionFactory;

        public MovieRepository(ISQLiteConnectionFactory connectionFactory)
        {
            if (_connection != null)
            {
                return;
            }
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateAsyncConnection();
        }

        public async Task AddAsync(T entity)
        {
            int result = 0;
            try
            {
                result = await _connection.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                //Debug log
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
