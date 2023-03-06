﻿using AutoMapper;
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
            var serviceResponse = new ServiceResponse<List<GetConversationDto>>();

            var conversation = _mapper.Map<Conversation>(newConversation);

            await _dataContext.AddAsync(conversation);
            await _dataContext.SaveChangesAsync();

            serviceResponse = await GetAll();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetConversationDto>>> Delete(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetConversationDto>>();
            try
            {
                var converstation = await _dataContext.Conversation.GetById(id);
                
                _dataContext.Remove(converstation);
                await _dataContext.SaveChangesAsync();

                serviceResponse = await GetAll();
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetConversationDto>>> GetAll()
        {
            var serviceResponse = new ServiceResponse<List<GetConversationDto>>();

            serviceResponse.Data = await _dataContext.Conversation.GetConversationDtoFromQuery(_mapper);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetConversationDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetConversationDto>();

            var converstation = await _dataContext.Conversation.GetById(id);
            serviceResponse.Data = _mapper.Map<GetConversationDto>(converstation);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetConversationDto>> Update(int id, UpdateConversationDto updateConversation)
        {
            var serviceResponse = new ServiceResponse<GetConversationDto>();

            try
            {
                var converstation = await _dataContext.Conversation.GetById(id);
                converstation = _mapper.Map<UpdateConversationDto, Conversation>(updateConversation, converstation);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetConversationDto>(converstation);
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}

