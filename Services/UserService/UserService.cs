﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupportTicketSystem.Data;
using SupportTicketSystem.Dtos.JoinUserTicket;
using SupportTicketSystem.Dtos.Ticket;
using SupportTicketSystem.Dtos.UserDtos;
using SupportTicketSystem.Services.JoinUserTicketService.ExtensionMethods;
using SupportTicketSystem.Services.TicketService.ExtensionMethods;
using SupportTicketSystem.Services.UserService.ExtensionMethods;

namespace SupportTicketSystem.Services.UserService
{
    public class UserService : IUserService
    {
        public IMapper _mapper { get; }
        public DataContext _dataContext { get; }

        public UserService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> Add(AddUserDto newUser)
        {
            // add user and save
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            
            User user = _mapper.Map<User>(newUser);

            await _dataContext.User.AddAsync(user);
            await _dataContext.SaveChangesAsync();

            return serviceResponse = await GetAll();
        }

        public async Task<ServiceResponse<List<GetUserDto>>> Delete(int id)
        {
            // delete user by id
            var serviceRepsonse = new ServiceResponse<List<GetUserDto>>();
            try
            {
                var user = await _dataContext.User.FirstAsync(u => u.Id == id);

                user.Archived = DateTime.Now;

                await _dataContext.SaveChangesAsync();

                serviceRepsonse = await GetAll();
            }
            catch (Exception ex)
            {
                serviceRepsonse.Succes = false;
                serviceRepsonse.Message = ex.Message;
            }
            return serviceRepsonse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAll()
        {
            //get all users
            var serviceRespone = new ServiceResponse<List<GetUserDto>>();

            serviceRespone.Data = await _dataContext.User.GetUserDtoFromQuery(_mapper);

            return serviceRespone;
        }

        public async Task<ServiceResponse<GetUserDto>> GetById(int id)
        {
            //get user by id
            var serviceResponse = new ServiceResponse<GetUserDto>();

            var user = await _dataContext.User.GetById(id);

            serviceResponse.Data = _mapper.Map<GetUserDto>(user);

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> Update(int id, UpdateUserDto updateUser)
        {
            //update user by id.
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _dataContext.User.GetById(id);
                user = _mapper.Map<UpdateUserDto, User>(updateUser, user);

                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateSecretView(int id, bool secretview)
        {
            // update secret view status
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _dataContext.User.GetById(id);

                user.SecretView = secretview;
                await _dataContext.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Succes = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTicketDto>>> GetAllCreatedTickets(int id)
        {
            // get tickets created by user.
            var serviceResponse = new ServiceResponse<List<GetTicketDto>>();
            
            List<Ticket> tickets = await _dataContext.Ticket.GetByCreatedId(id);

            serviceResponse.Data = _mapper.Map<List<GetTicketDto>>(tickets);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetJoinUserTicketDto>>> GetAllInvolvedTickets(int id)
        {
            // get tickets that the user is involved with.
            var serviceResponse = new ServiceResponse<List<GetJoinUserTicketDto>>();

            List<JoinUserTicket> involvedUsers = await _dataContext.JoinUserTicket.GetByUserId(id);

            serviceResponse.Data = _mapper.Map<List<GetJoinUserTicketDto>>(involvedUsers);

            return serviceResponse;
        }


    }
}
