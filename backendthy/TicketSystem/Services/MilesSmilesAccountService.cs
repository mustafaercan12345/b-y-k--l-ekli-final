using TicketSystem.Models;
using TicketSystem.Repositories;

namespace TicketSystem.Services
{
    public class MilesSmilesAccountService : IMilesSmilesAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public MilesSmilesAccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public MilesSmilesAccount AddMilesSmileAccount(int userId)
        {
            var milesAccount = new MilesSmilesAccount { 
            Id = 0,
            UserId = userId,
            Miles = 0,
            
            };
            _context.MilesSmilesAccounts.Add(milesAccount);
            _context.SaveChanges();
            return milesAccount;
        }

        public void UpdateMiles(int userId, int miles)
        {
            var milesAccount = _context.MilesSmilesAccounts.FirstOrDefault(account => account.UserId == userId);

            if (milesAccount != null)
            {
                milesAccount.Miles = miles;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("User not found.");
            }
        }
    }
}
