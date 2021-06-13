using Microsoft.Extensions.Configuration;
using MISA.AMIS.Core.Entities;
using MySql.Data.MySqlClient;

namespace MISA.AMIS.Infrastructure.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public class DepartmentRepository : BaseRepository<Department>
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
            this._connectionString = configuration.GetConnectionString("DefaultDb");

            _dbConnection = new MySqlConnection(_connectionString);
        }
    }
}