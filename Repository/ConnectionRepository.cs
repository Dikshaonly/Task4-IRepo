using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;

namespace Task4.Repository{
    public class ConnectionRepository : IConnectionRepository{
        private readonly IConfiguration _config;
        public ConnectionRepository(IConfiguration config){
            _config = config;
        }
        public string GetCS(string connectionId = "DefaultConnection")
  {
             
      return _config.GetConnectionString(connectionId)!;
  }
    }
}