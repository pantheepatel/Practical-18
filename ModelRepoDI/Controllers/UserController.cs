using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;
using ViewModels.Repository.AuthRepo;

namespace ViewModels.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserRepository userRepository, IMapper mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userRepository.GetAllAsync();
            var userVMs = mapper.Map<List<UserViewModel>>(users);
            return Ok(userVMs);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserViewModel>(user));
        }
        [HttpPost]
        public async Task<IActionResult> Create(User userData)
        {
            var user = mapper.Map<User>(userData);
            await userRepository.AddAsync(user);
            var userVM = mapper.Map<UserViewModel>(user);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, userVM);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User userData)
        {
            if (id != userData.Id)
            {
                return BadRequest();
            }
            var user = mapper.Map<User>(userData);
            user.Id = id;
            await userRepository.UpdateAsync(user);
            return Ok(new { message = "User updated successfully" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await userRepository.DeleteAsync(id);
            return Ok(new { message = "User deleted successfully" });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await userRepository.AuthenticateAsync(email, password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(mapper.Map<UserViewModel>(user));
        }
    }
}
