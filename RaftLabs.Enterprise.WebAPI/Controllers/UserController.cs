using Microsoft.AspNetCore.Mvc;
using RaftLabs.Enterprise.Domain.DTOs;
using RaftLabs.Enterprise.Domain.Models;
using RaftLabs.Enterprise.WebAPI.Interfaces.Services;

namespace RaftLabs.Enterprise.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService) 
        { 
            this.userService = userService;
        }

        [HttpGet("Get")]
        public Task<UserDTO> GetUserByIdAsync(string userId)
        {
            return userService.GetUserByIdAsync(userId);
        }

        [HttpGet("GetAll")]
        public Task<IEnumerable<UserDTO>> GetAllUsersAsync(string pageNumber)
        {
            return userService.GetAllUsersAsync(pageNumber);
        }
  
    }
}
