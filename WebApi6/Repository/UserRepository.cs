using WebApi6.Data;

namespace WebApi6.Repository
{
    public class UserRepository : IUserRepository
    {
        private PatienDbContext _context;

        public UserRepository(PatienDbContext context)
        {
            _context = context;
        }

        public User GetUser(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public User ValidateUser(string username, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
