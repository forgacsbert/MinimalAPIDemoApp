using DataAccess.DbAccess;

namespace DataAccess.Data;

public class UserData
{
    private readonly ISQLDataAccess _db;

    public UserData(ISQLDataAccess db)
    {
        _db = db;
    }


}
