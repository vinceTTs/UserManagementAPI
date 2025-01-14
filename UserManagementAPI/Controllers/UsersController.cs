using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;
using UserManagementAPI.Validators;

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
            try
            {
                return Ok(_userRepository.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{index}")]
        public ActionResult<User> GetUser(int index)
        {
            try
            {
                var user = _userRepository.GetByIndex(index);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<User> AddUser(User user)
        {
            try
            {
                if (!UserValidator.IsValid(user, out string errorMessage))
                {
                    return BadRequest(errorMessage);
                }

                _userRepository.Add(user);
                return CreatedAtAction(nameof(GetUser), new { index = _userRepository.GetAll().Count() - 1 }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{index}")]
        public IActionResult UpdateUser(int index, User user)
        {
            try
            {
                if (!UserValidator.IsValid(user, out string errorMessage))
                {
                    return BadRequest(errorMessage);
                }

                var existingUser = _userRepository.GetByIndex(index);
                if (existingUser == null)
                {
                    return NotFound();
                }

                _userRepository.Update(index, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{index}")]
        public IActionResult DeleteUser(int index)
        {
            try
            {
                var user = _userRepository.GetByIndex(index);
                if (user == null)
                {
                    return NotFound();
                }

                _userRepository.Delete(index);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}