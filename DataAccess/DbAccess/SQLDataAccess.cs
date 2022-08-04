using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess.DbAccess;
public class SQLDataAccess : ISQLDataAccess
{
    private readonly IConfiguration _configuration;

    public SQLDataAccess(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Method used to retrieve data from the database.
    /// </summary>
    /// <typeparam name="T">Type of data to be loaded</typeparam>
    /// <typeparam name="U">Generic Parameters</typeparam>
    /// <param name="storedProcedure">Stored procedure to execute</param>
    /// <param name="parameters">Parameters to be used when executing the stored procedure.</param>
    /// <param name="connectionId">The id of the connection found in the configurations</param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> LoadData<T, U>(
        string storedProcedure,
        U parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Method used to save data into the database.
    /// </summary>
    /// <typeparam name="T">Type of data to be saved.</typeparam>
    /// <param name="storedProcedure">Stored procedure to be executed.</param>
    /// <param name="parameters">Parameters to be specified when executing the stored procedure.</param>
    /// <param name="connectionId">The id of the connection found in the configurations.</param>
    /// <returns></returns>
    public async Task SaveData<T>(
        string storedProcedure,
        T parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }
}
