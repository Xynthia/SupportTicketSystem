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
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetById(int id)
        {
            return Ok(await _userService.GetById(id)); 
        }

        [HttpGet("{id}/Tickets")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllTickets(int id)
        {
            return Ok(await _userService.GetAllTickets(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Add(AddUserDto newUser)
        {
            return Ok(await _userService.Add(newUser));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> Delete(int id)
        {
            return Ok(await _userService.Delete(id));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Update(int id, UpdateUserDto updateUser)
        {
            return Ok(await _userService.Update(id, updateUser)); 
        }

        [HttpPut("{id}/SecretView/True")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateSecretViewTrue(int id)
        {
            return Ok(await _userService.UpdateSecretView(id, true));
        }

        [HttpPut("{id}/SecretView/False")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateSecretViewFalse(int id)
        {
            return Ok(await _userService.UpdateSecretView(id, false));
        }
    }
}
