using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportTicketSystem.Dtos.Conversation;
using SupportTicketSystem.Services.ConversationService;

namespace SupportTicketSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        public IConversationService _conversationService { get; }
        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetConversationDto>>>> GetAll()
        {
            return Ok(await _conversationService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetConversationDto>>> GetByID(int id)
        {
            return Ok(await _conversationService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetConversationDto>>>> Add(AddConversationDto newConversation)
        {
            return Ok( await _conversationService.Add(newConversation));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetConversationDto>>> Update(int id, UpdateConversationDto updateConversation)
        {
            var serviceResponse = await _conversationService.Update(id, updateConversation);
            if(serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<GetConversationDto>>> Delete(int id)
        {
            var serviceResponse = await _conversationService.Delete(id);
            if (serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }
            return Ok(serviceResponse);
        }
    }
}
