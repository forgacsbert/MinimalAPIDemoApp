using DataAccess.DbAccess;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalAPIDemo.EndpointDefinitions.Extensions;

namespace MinimalAPIDemo.EndpointDefinitions;

public class UserEndpointDefinition : IEndpointDefinition
{
    public bool ShouldRunLast => false;

    /// <summary>
    /// Method used to configure the application with the necessary endpoints.
    /// </summary>
    /// <param name="app">Represents the app that will be configured.</param>
    public void DefineEndpoints(WebApplication app)
    {
        // All of the user endpoint mappings will be defined here.

        app.MapGet("/Users", GetUsers);
        app.MapGet("/Users/{id}", GetUser);
        app.MapPost("/Users", InsertUser).AllowAnonymous();
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
    [ProducesResponseType(200, Type = (typeof(UserModel)))]
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
    [ProducesResponseType(200, Type = (typeof(UserModel)))]
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
    private async Task<IResult> InsertUser(UserModel user, IValidator<UserModel> validator, IUserData data)
    {
        try
        {
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => new { errors = x.ErrorMessage });
                return Results.BadRequest(errors);
            }

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
