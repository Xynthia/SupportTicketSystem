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

        [HttpGet("{id}/Tickets/Created")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllCreatedTickets(int id)
        {
            return Ok(await _userService.GetAllCreatedTickets(id));
        }

        [HttpGet("{id}/Tickets/Involved")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllTickets(int id)
        {
            return Ok(await _userService.GetAllInvolvedTickets(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Add(AddUserDto newUser)
        {
            return Ok(await _userService.Add(newUser));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> Delete(int id)
        {
            var serviceResponse = await _userService.Delete(id);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> Update(int id, UpdateUserDto updateUser)
        {
            var serviceResponse = await _userService.Update(id, updateUser);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpPut("{id}/SecretView/True")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateSecretViewTrue(int id)
        {
            var serviceResponse = await _userService.UpdateSecretView(id, true);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpPut("{id}/SecretView/False")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> UpdateSecretViewFalse(int id)
        {
            var serviceResponse = await _userService.UpdateSecretView(id, false);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
    }
}
