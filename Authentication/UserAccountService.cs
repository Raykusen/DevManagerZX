using DevManager.Data.Context;
using DevManager.Data.Models;

namespace Authentication;

public interface IUserAccountService
{
    User? GetByUserName(string userName);
}
public class UserAccountService : IUserAccountService
{
    //private List<UserAccount> _users;

    //public UserAccountService()
    //{
       // _users = new List<UserAccount>
        //{
        //    new UserAccount{UserName = "admin", Password = "123", Role = "Administrator"},
        //    new UserAccount{UserName = "user", Password = "user", Role = "User"}
        //};
    //}

    #region Constructor y mienbro privado
    private AppDbContext _database;

    public UserAccountService(AppDbContext database)
    {
        _database = database;
    }
    #endregion
    public User? GetByUserName(string userName)
    {
        return _database.Users.FirstOrDefault(x => x.Email == userName);
    }
}