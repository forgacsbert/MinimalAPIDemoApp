namespace MinimalAPIDemo;

public static class Api
{
    /// <summary>
    /// Method used to configure the application with the necessary endpoints.
    /// </summary>
    /// <param name="app">Represents the app that will be configured.</param>
    public static void ConfigureApi(this WebApplication app)
    {
        // All of the endpoint mappings will be defined here.

        app.MapGet("/Users", GetUsers);
        app.MapGet("/Users/{id}", GetUser);
        app.MapPost("/Users", InsertUser);
        app.MapPut("/Users", UpdateUser);
        app.MapDelete("/Users", DeleteUser);
    }

    /// <summary>
    /// Gets all users
    /// </summary>
    /// <param name="data">user data access</param>
    /// <returns>all users retrieved from the users data</returns>
    private static async Task<IResult> GetUsers(IUserData data)
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
    private static async Task<IResult> GetUser(int id, IUserData data)
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
    private static async Task<IResult> InsertUser(UserModel user, IUserData data)
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
    private static async Task<IResult> UpdateUser(UserModel user, IUserData data)
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
    private static async Task<IResult> DeleteUser(int id, IUserData data)
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
