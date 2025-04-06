using ViewModels.Models;

namespace ViewModels.Repository.AuthRepo
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User> AuthenticateAsync(string email, string password); //login
    }
}
