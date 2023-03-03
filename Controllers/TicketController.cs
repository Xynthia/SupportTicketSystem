using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Services.TicketService;

namespace SupportTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        public ITicketService _ticketService { get; }

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public  async Task<ActionResult<ServiceResponse<List<GetTicketDto>>>> GetAll()
        {
            return Ok(await _ticketService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTicketDto>>> GetById(int id)
        {
            return Ok(await _ticketService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetTicketDto>>>> Add(AddTicketDto newTicket)
        {
            return Ok(await _ticketService.Add(newTicket));
        }

        [HttpPost("{id}/involvedUsers")]
        public async Task<ActionResult<ServiceResponse<List<GetTicketDto>>>> AddUsersInvolved(AddJoinUserTicketDto newJoinUserTicket)
        {
            return Ok(await _ticketService.AddUsersInvolved(newJoinUserTicket));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTicketDto>>> Update(int id, UpdateTicketDto updateTicket)
        {
            var serviceResponse = await _ticketService.Update(id, updateTicket);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetTicketDto>>>> Delete(int id)
        {
            var serviceResponse = await _ticketService.Delete(id);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpPut("{id}/Status")]
        public async Task<ActionResult<ServiceResponse<GetTicketDto>>> UpdateStatus(int id, UpdateTicketDto updateTicket)
        {
            var serviceResponse = await _ticketService.UpdateStatus(id, updateTicket);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

    }
}
