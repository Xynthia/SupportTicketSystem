using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Services.JoinUserTicketService;

namespace SupportTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinUserTicketController : ControllerBase
    {
        public JoinUserTicketController(IJoinUserTicketService joinUserTicketService)
        {
            _joinUserTicketService = joinUserTicketService;
        }

        public IJoinUserTicketService _joinUserTicketService { get; }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetJoinUserTicketDto>>>> GetAll()
        {
            return Ok(await _joinUserTicketService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetJoinUserTicketDto>>> GetByID(int id)
        {
            return Ok(await _joinUserTicketService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetJoinUserTicketDto>>>> Add(AddJoinUserTicketDto newJoinUserTicket)
        {
            return Ok(await _joinUserTicketService.Add(newJoinUserTicket));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetJoinUserTicketDto>>> Update(int id, UpdateJoinUserTicketDto updateJoinUserTicket)
        {
            var serviceResponse = await _joinUserTicketService.Update(id, updateJoinUserTicket);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<GetJoinUserTicketDto>>> Delete(int id)
        {
            var serviceResponse = await _joinUserTicketService.Delete(id);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

    }
}
