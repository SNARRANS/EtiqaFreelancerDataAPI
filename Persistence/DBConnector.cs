using System.Data.SqlClient;

namespace EtiqaFreelancerDataAPI.Persistence
{
    public class DBConnector : IDisposable
    {
        public SqlConnection connection;

        public DBConnector(string? connectionString)
        {
            connection = new SqlConnection(connectionString);
            this.connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
