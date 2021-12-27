using WebApi6.Data;

namespace WebApi6.Repository
{
    public interface IUserRepository
    {
        User GetUser(string username);

        User ValidateUser(string username, string password);
    }
}
