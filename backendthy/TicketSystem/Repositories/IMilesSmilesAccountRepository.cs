using TicketSystem.Models;

namespace TicketSystem.Repositories
{
    public interface IMilesSmilesAccountRepository
    {
        void UpdateMiles(int userId, int miles);
        MilesSmilesAccount AddMilesSmileAccount(int userId);
    }
}
