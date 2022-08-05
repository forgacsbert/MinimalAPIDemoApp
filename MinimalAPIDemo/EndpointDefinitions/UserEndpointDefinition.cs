using DataAccess.DbAccess;
using MinimalAPIDemo.EndpointDefinitions.Extensions;

namespace MinimalAPIDemo.EndpointDefinitions;

public class UserEndpointDefinition : IEndpointDefinition
{
    /// <summary>
    /// Method used to configure the application with the necessary endpoints.
    /// </summary>
    /// <param name="app">Represents the app that will be configured.</param>
    public void DefineEndpoints(WebApplication app)
    {
        // All of the user endpoint mappings will be defined here.

        app.MapGet("/Users", GetUsers);
        app.MapGet("/Users/{id}", GetUser);
        app.MapPost("/Users", InsertUser);
        app.MapPut("/Users", UpdateUser);
        app.MapDelete("/Users", DeleteUser);
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
        services.AddSingleton<IUserData, UserData>();
    }

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <param name="data">user data access</param>
    /// <returns>all users retrieved from the users data</returns>
    private async Task<IResult> GetUsers(IUserData data)
    {
        try
        {
            return Results.Ok(await data.GetUsers());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    /// <summary>
    /// Gets a user based on the id.
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <param name="data">user data access</param>
    /// <returns>found user</returns>
    private async Task<IResult> GetUser(int id, IUserData data)
    {
        try
        {
            var results = await data.GetUser(id);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    /// <summary>
    /// Insert user
    /// </summary>
    /// <param name="user">user</param>
    /// <param name="data">data</param>
    /// <returns>result</returns>
    private async Task<IResult> InsertUser(UserModel user, IUserData data)
    {
        try
        {
            await data.InsertUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="user">user</param>
    /// <param name="data">data</param>
    /// <returns>Result</returns>
    private async Task<IResult> UpdateUser(UserModel user, IUserData data)
    {
        try
        {
            await data.UpdateUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    /// <summary>
    /// Delete user
    /// </summary>
    /// <param name="id">id of user</param>
    /// <param name="data">data</param>
    /// <returns>result</returns>
    private async Task<IResult> DeleteUser(int id, IUserData data)
    {
        try
        {
            await data.DeteleUser(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
