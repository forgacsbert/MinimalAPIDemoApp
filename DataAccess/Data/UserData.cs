using DataAccess.DbAccess;
using DataAccess.Models;

namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISQLDataAccess _db;

    public UserData(ISQLDataAccess db)
    {
        _db = db;
    }

    /// <summary>
    /// Method used to get all the users.
    /// </summary>
    /// <returns>Returns the collection of users.</returns>
    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });

    /// <summary>
    /// Method used to get a user based on its id.
    /// </summary>
    /// <param name="id">Represents the id of the user to retrieve.</param>
    /// <returns>User found under the given id.</returns>
    public async Task<UserModel> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>(
            "dbo.spUser_Get",
            new { Id = id });

        return results.FirstOrDefault();
    }

    public Task InsertUser(UserModel user) =>
        _db.SaveData("dbo.spUser_Insert", new { user.FirstName, user.LastName });

    public Task UpdateUser(UserModel user) =>
        _db.SaveData("dbo.spUser_Update", user);

    public Task DeteleUser(int id) =>
        _db.SaveData("dbo.spUser_Delete", new { Id = id });
}
