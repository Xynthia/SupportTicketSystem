using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<GetTicketDto>>> Update(int id, UpdateTicketDto updateTicket)
        {
            return Ok(await _ticketService.Update(id, updateTicket));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<GetTicketDto>>>> Delete(int id)
        {
            return Ok(await _ticketService.Delete(id));
        }

        [HttpPut("{id}/Status")]
        public async Task<ActionResult<ServiceResponse<GetTicketDto>>> UpdateStatus(int id, UpdateTicketDto updateTicket)
        {
            return Ok(await _ticketService.UpdateStatus(id, updateTicket));
        }

    }
}
