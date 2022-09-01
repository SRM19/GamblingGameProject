using AutoMapper;
using Microsoft.EntityFrameworkCore;
using GamblingGame.DbContexts;
using GamblingGame.Models;
using GamblingGame.Models.DTO;

namespace GamblingGame.Repository
{
    public class GambleRepository : IGambleRepository
    {
        private AppDbContext _db;
        private IMapper _mapper;

        public GambleRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<UsersDto> GetUserById(int userId)
        {
            User user = await _db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            return _mapper.Map<UsersDto>(user);
        }

        public async Task<UsersDto> GetUserByName(string name)
        {
            User user = await _db.Users.Where(x => x.Username == name).FirstOrDefaultAsync();
            return _mapper.Map<UsersDto>(user);
        }

        public async Task<AccountsDto> GetAccount(int userId)
        {
            Account userAccount = await _db.Accounts.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            //foreign key constraint
            _db.Entry(userAccount).State = EntityState.Detached;
            return _mapper.Map<AccountsDto>(userAccount);
        }
        public async Task<AccountsDto> UpdateAccount(AccountsDto accountsDto)
        {
            try
            {
                Account account = _mapper.Map<Account>(accountsDto);
                _db.Accounts.Update(account);
                await _db.SaveChangesAsync();
                return _mapper.Map<AccountsDto>(account);
            }
            catch(Exception)
            {
                throw;
            }

        }

    }
}
