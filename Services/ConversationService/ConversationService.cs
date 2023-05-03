using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Dtos.Conversation;
using SupportTicketSystem.Services.ConversationService.ExtensionMethods;

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
            //add conversation and save
            var serviceResponse = new ServiceResponse<List<GetConversationDto>>();

            var conversation = _mapper.Map<Conversation>(newConversation);

            int startIndex = 0;
            int endIndex = 0;

            // finding id from toUser && ticket
            startIndex = conversation.Log.IndexOf("To") + 6;
            endIndex = conversation.Log.IndexOf(".tickets");
            int ticketId = Int32.Parse(conversation.Log.Substring(startIndex, endIndex - startIndex));
            var ticket = await _dataContext.Ticket.FirstOrDefaultAsync(t => t.Id == ticketId);
            conversation.TicketId = ticket.Id;
            conversation.ToUserId = ticket.ResponsibleForID;

            //finding id from fromUser
            startIndex = conversation.Log.IndexOf("From") + 8;
            endIndex = conversation.Log.IndexOf("To") - 6;
            string fromUserEmail = conversation.Log.Substring(startIndex, endIndex - startIndex);
            var user = await _dataContext.User.FirstOrDefaultAsync(x => x.Email == fromUserEmail);
            conversation.FromUserId = user.Id;

            // if ticket and user excist then add conversation.
            if (user != null && ticket != null)
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
                var converstation = await _dataContext.Conversation.GetById(id);
                
                _dataContext.Remove(converstation);
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

            serviceResponse.Data = await _dataContext.Conversation.GetConversationDtoFromQuery(_mapper);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetConversationDto>> GetById(int id)
        {
            // get conversation by id
            var serviceResponse = new ServiceResponse<GetConversationDto>();

            var converstation = await _dataContext.Conversation.GetById(id);
            serviceResponse.Data = _mapper.Map<GetConversationDto>(converstation);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetConversationDto>> Update(int id, UpdateConversationDto updateConversation)
        {
            var serviceResponse = new ServiceResponse<GetConversationDto>();

            //update conversation by id.
            try
            {
                var converstation = await _dataContext.Conversation.GetById(id);
                converstation = _mapper.Map<UpdateConversationDto, Conversation>(updateConversation, converstation);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetConversationDto>(converstation);
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

            int startIndex = 0;
            int endIndex = 0;

            //update log of conversation
            try
            {
                var conversation = await _dataContext.Conversation.GetById(id);
                startIndex = conversation.Log.IndexOf("StrippedTextReply") + 23;
                conversation.Log = conversation.Log.Insert(startIndex, updateLog + "___ reply before this line ___ ");

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

