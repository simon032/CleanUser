using CleanUser.ApplicationCore.DTOs;
using CleanUser.ApplicationCore.Exceptions;
using CleanUser.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanUser.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<List<UserResponse>> GetUsers()
        {
            return Ok(_userRepository.GetUsers());
        }

        [HttpGet("{id}")]
        public ActionResult GetUserById(int id)
        {
            try
            {
                var user = _userRepository.GetUserById(id);
                return Ok(user);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Create(CreateUserRequest request)
        {
            var user = _userRepository.CreateUser(request);
            return Ok(user);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, CreateUserRequest request)
        {
            try
            {
                var user = _userRepository.UpdateUser(id, request);
                return Ok(user);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _userRepository.DeleteUserById(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}