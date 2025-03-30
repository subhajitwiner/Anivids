using api.Models;

namespace api.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserModel>> GetUsersAsync();
        Task<UserModel?> GetUserByUserNameAsync(string username);
        Task<UserModel?> GetUserByIdAsync(int Id);
    }
}
