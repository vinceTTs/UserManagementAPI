using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userRepository.GetAll());
        }

        [HttpGet("{index}")]
        public ActionResult<User> GetUser(int index)
        {
            var user = _userRepository.GetByIndex(index);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            _userRepository.Add(user);
            return CreatedAtAction(nameof(GetUser), new { index = _userRepository.GetAll().Count() - 1 }, user);
        }

        [HttpPut("{index}")]
        public IActionResult UpdateUser(int index, User user)
        {
            var existingUser = _userRepository.GetByIndex(index);
            if (existingUser == null)
            {
                return NotFound();
            }

            _userRepository.Update(index, user);
            return NoContent();
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteUser(int index)
        {
            var user = _userRepository.GetByIndex(index);
            if (user == null)
            {
                return NotFound();
            }

            _userRepository.Delete(index);
            return NoContent();
        }
    }
}