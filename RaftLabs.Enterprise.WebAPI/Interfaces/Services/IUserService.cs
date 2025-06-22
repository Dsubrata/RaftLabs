using RaftLabs.Enterprise.Domain.DTOs;
using RaftLabs.Enterprise.Domain.Models;

namespace RaftLabs.Enterprise.WebAPI.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {
        Task<UserDTO> GetUserByIdAsync(string userId);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync(string pageNumber);
        
    }
}
