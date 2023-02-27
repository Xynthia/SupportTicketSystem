using AutoMapper;
using SupportTicketSystem.Dtos.UserDtos;

namespace SupportTicketSystem.Services.UserService
{
    public class UserService : IUserService
    {
        static public List<User> users = new List<User>
        {
            new User(),
            new User{ Id = 1, Name = "Xynthia"}
        };

        public IMapper _mapper { get; }

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<ServiceResponse<List<GetUserDto>>> Add(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            
            User user = _mapper.Map<User>(newUser);

            users.Add(user);

            serviceResponse.Data = users.Select(u => _mapper.Map<GetUserDto>(u)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> Delete(int id)
        {
            var serviceRepsonse = new ServiceResponse<List<GetUserDto>>();
            try
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                users.Remove(user);
                serviceRepsonse.Data = users.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
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
            var serviceRespone = new ServiceResponse<List<GetUserDto>>();
            serviceRespone.Data = users.Select(u => _mapper.Map<GetUserDto>(u)).ToList();
            return serviceRespone;
        }

        public async Task<ServiceResponse<GetUserDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var user = users.FirstOrDefault(u => u.Id == id);
            serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> Update(UpdateUserDto updateUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = users.FirstOrDefault(u => u.Id == updateUser.Id);

                user = _mapper.Map<UpdateUserDto, User>(updateUser, user);

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
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
