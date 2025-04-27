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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                if (_connection == null)
                    throw new InvalidOperationException("Connection to database could not be established.");

                var list = await _connection.Table<T>().ToListAsync();
                return list;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<T>();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _connection.FindAsync<T>(id.ToString());
            }
            catch (Exception ex)
            {
                return default!;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                await _connection.UpdateAsync(entity);
            }
            catch (Exception ex)
            {

            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                await _connection.DeleteAsync(entity);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
