using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Models;
using ViewModels.Repository.AuthRepo;
using ViewModels.Repository.StudentRepo;

namespace ViewModels.Controllers
{
    public class UserController(IUserRepository userRepository, IMapper mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var users = await userRepository.GetAllAsync();
            var userVMs = mapper.Map<List<UserViewModel>>(users);
            return View(userVMs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var viewModel = mapper.Map<UserViewModel>(user);
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,MobileNumber,Password,RoleId")] User userData)
        {
            var user = mapper.Map<User>(userData);
            await userRepository.AddAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            var viewModel = mapper.Map<UserViewModel>(user);
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,MobileNumber,Password,RoleId")] User userData)
        {
            if (id != userData.Id)
            {
                return BadRequest();
            }
            var user = mapper.Map<User>(userData);
            user.Id = id;
            await userRepository.UpdateAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await userRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = await userRepository.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(mapper.Map<UserViewModel>(user));
        }
    }
}
