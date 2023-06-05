using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Dtos.Conversation;
using SupportTicketSystem.Services.ConversationService.ExtensionMethods;
using SupportTicketSystem.Services.GuardService;
using System.Text.Json;
using System.Web;

namespace SupportTicketSystem.Services.ConversationService
{
    public class ConversationService : IConversationService
    {
        public DataContext _dataContext { get; }
        public IMapper _mapper { get; }

        public ConversationService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetConversationDto>>> Add(AddConversationDto newConversation)
        {
            var serviceResponse = new ServiceResponse<List<GetConversationDto>>();

            var conversation = _mapper.Map<Conversation>(newConversation);

            var postmarkLog = JsonSerializer.Deserialize<PostmarkLogModel>(conversation.Log);

            var ticketIdSubstring = postmarkLog.To.Substring(0, postmarkLog.To.IndexOf(".tickets"));
            int ticketId = Int32.Parse(ticketIdSubstring);
            var ticket = await _dataContext.Ticket.FirstOrDefaultAsync(t => t.Id == ticketId);
            Guard.Against.Null(ticket);

            var FromUserEmail = postmarkLog.From;
            var fromUser = await _dataContext.User.FirstOrDefaultAsync(x => x.Email == FromUserEmail);
            Guard.Against.Null(fromUser);

            conversation.FromUserId = fromUser.Id;
            conversation.TicketId = ticket.Id;
            conversation.ToUserId = ticket.ResponsibleForID;

            // if ticket and user excist then add conversation.
            if (fromUser != null && ticket != null)
            {
                await _dataContext.Conversation.AddAsync(conversation);
                await _dataContext.SaveChangesAsync();
            }

            serviceResponse = await GetAll();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetConversationDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetConversationDto>>();

            //remove conversation by id
            try
            {
                var conversation = await _dataContext.Conversation.GetById(id);
                Guard.Against.Null(conversation);

                //converstion is being archieved. we can see the time and date. if it is archieved or not.
                conversation.Archived = DateTime.Now;

                await _dataContext.SaveChangesAsync();

                serviceResponse = await GetAll();
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetConversationDto>>> GetAll()
        {
            // get all conversations
            var serviceResponse = new ServiceResponse<List<GetConversationDto>>();

            var conversations = await _dataContext.Conversation.GetConversationDtoFromQuery(_mapper);
            Guard.Against.Null(conversations);

            serviceResponse.Data = conversations;

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetConversationDto>> GetById(int id)
        {
            // get conversation by id
            var serviceResponse = new ServiceResponse<GetConversationDto>();

            var conversation = await _dataContext.Conversation.GetById(id);
            Guard.Against.Null(conversation);
            serviceResponse.Data = _mapper.Map<GetConversationDto>(conversation);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetConversationDto>> Update(int id, UpdateConversationDto updateConversation)
        {
            var serviceResponse = new ServiceResponse<GetConversationDto>();

            //update conversation by id.
            try
            {
                var conversation = await _dataContext.Conversation.GetById(id);
                Guard.Against.Null(conversation);
                conversation = _mapper.Map<UpdateConversationDto, Conversation>(updateConversation, conversation);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetConversationDto>(conversation);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetConversationDto>> UpdateLog(int id, string updateLog)
        {
            var serviceResponse = new ServiceResponse<GetConversationDto>();

            //update log of conversation
            try
            {
                var conversation = await _dataContext.Conversation.GetById(id);
                Guard.Against.Null(conversation);

                var postmarkLog = JsonSerializer.Deserialize<PostmarkLogModel>(conversation.Log);

                postmarkLog.StrippedTextReply = postmarkLog.StrippedTextReply.Insert(0, "\n" + updateLog + "\n ___ reply before this line ___ \n");

                conversation.Log = JsonSerializer.Serialize<PostmarkLogModel>(postmarkLog);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetConversationDto>(conversation);
            }
            catch (Exception ex)
            {
                //send message when it goes wrong.
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}

