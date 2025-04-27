using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MovieRepoApp.Models;
using SQLite;

namespace MovieRepoApp.Services.Repo
{
    public class MovieRepository<T> : IRepository<T> where T : new()
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
            int result = await _connection.InsertAsync(entity);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (_connection == null)
                throw new InvalidOperationException("Connection to database could not be established.");

            var list = await _connection.Table<T>().ToListAsync();
            return list;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _connection.FindAsync<T>(id.ToString());
        }

        public async Task UpdateAsync(T entity)
        {
            await _connection.UpdateAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _connection.DeleteAsync(entity);
        }

        public async Task<IEnumerable<T>> QueryAsync(string query)
        {
            return await _connection.QueryAsync<T>(query);
        }
    }
}
