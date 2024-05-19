using EtiqaFreelancerDataAPI.Models;
using EtiqaFreelancerDataAPI.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace EtiqaFreelancerDataAPI.Data;

public interface ITokenRepository
{

    bool IsLogin(string Username, string Userpassword);
}
public class TokenRepository : ITokenRepository
{    
    private ApplicationDBContext _dbContext;
    private DBConnector _connector;

    public TokenRepository(ApplicationDBContext context, DBConnector dBConnector)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _connector = dBConnector;
    }


    public bool IsLogin(string Username, string Userpassword)
    {
        var result = false;
        try
        {
            if (this._connector.connection.State == ConnectionState.Closed)
                this._connector.connection.Open();
            var cmd = this._connector.connection.CreateCommand() as SqlCommand;
            //cmd.CommandText = @"SELECT Id, Name, Price, Quantity, RowVersion FROM Products WHERE Id = @Id";
            cmd.CommandText = @"select [Id] from [dbo].[LoginInfo] where [Username] = @1 and [Userpassword] = @2 ;";
            cmd.Parameters.AddWithValue("@1", Username);
            cmd.Parameters.AddWithValue("@2", Userpassword);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = true;
            }
        }
        catch (Exception)
        {
            //throw ex;
            result = false;
        }
        finally
        {
            // close connection
            this._connector.Dispose();
        }

        // Task.FromResult is a helper method that creates a Task that's completed successfully
        // with the specified result.
        return result;

    }



}
