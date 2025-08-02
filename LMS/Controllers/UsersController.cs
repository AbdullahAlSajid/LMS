using LMS.Data;
using LMS.Models.Dtos;
using LMS.Models.Entities;
using LMS.Services.UserModule;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) 
        {
            _userService = userService;
        }
         
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _userService.CreateUser(userDto);

            return CreatedAtAction(
                nameof(GetUserById),
                new { id = created.Id },
                created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserDto updatedUser)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _userService.UpdateUser(id, updatedUser);

            if (result == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser (int id)
        {
            var success = await _userService.DeleteUser(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}


