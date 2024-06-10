using TicketSystem.Models;

namespace TicketSystem.Repositories
{
    public interface IUserRepository
    {
        User? Login(string email, string password);
        User AddUser(User user);
        string GenerateToken();

        User GetUserById(int id);

        void PublishWelcomeEmailMessage(User user);
    }
}
