using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MovieRepoApp.Services.Repo
{
    public class SQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        private readonly string _dbPath;
        public SQLiteConnectionFactory(string dbPath)
        {
            _dbPath = dbPath;
        }

        public SQLiteAsyncConnection CreateAsyncConnection()
        {
            return new SQLiteAsyncConnection(_dbPath);
        }

        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(_dbPath);
        }
    }
}
