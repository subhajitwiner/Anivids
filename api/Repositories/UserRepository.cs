using api.DataConfig;
using api.Interfaces.Repositories;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Reposetories
{
    public class UserRepository(DataContext context): IUserRepository
    {

        public async Task<IEnumerable<UserModel>> GetUsersAsync() {
            return await context.Users.ToListAsync();
        }
        public async Task<UserModel?> GetUserByUserNameAsync(string username)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.UserName == username);
        }

        public async Task<UserModel?> GetUserByIdAsync(int Id)
        {
            return await context.Users.FindAsync(Id);
        }
    }
}
