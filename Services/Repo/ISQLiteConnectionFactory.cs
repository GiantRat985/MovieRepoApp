using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MovieRepoApp.Services.Repo
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteConnection CreateConnection();
        SQLiteAsyncConnection CreateAsyncConnection();
    }
}
