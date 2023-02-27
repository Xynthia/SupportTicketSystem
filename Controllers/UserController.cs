using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Dtos.UserDtos;

namespace SupportTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService { get; }
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetById(int id)
        {
            return Ok(_userService.GetById(id)); 
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<User>>> Add(AddUserDto newUser)
        {
            return Ok(_userService.Add(newUser));
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok(_userService.Delete(id));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<User>>> Update(UpdateUserDto updateUser)
        {
            return Ok(_userService.Update(updateUser)); 
        }
    }
}
