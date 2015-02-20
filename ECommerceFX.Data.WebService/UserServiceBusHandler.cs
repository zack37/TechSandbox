using ECommerceFX.Data.Messages.Commands;
using ECommerceFX.Data.Messages.Events;
using ECommerceFX.Data.Messages.Queries;
using ECommerceFX.Repository;
using NServiceBus;

namespace ECommerceFX.Data.WebService
{
    public class UserServiceBusHandler : IHandleMessages<GetAllUsersQuery>,
                                         IHandleMessages<GetUserByIdQuery>,
                                         IHandleMessages<GetUserByEmailQuery>,
                                         IHandleMessages<GetUserByUsernameQuery>,
                                         IHandleMessages<CreateUser>
    {
        private readonly IBus _bus;
        private readonly IUserRepository _userRepository;

        public UserServiceBusHandler(IBus bus, IUserRepository userRepository)
        {
            _bus = bus;
            _userRepository = userRepository;
        }
        
        public void Handle(GetAllUsersQuery message)
        {
            var response = new GetAllUsersResponse
            {
                Users = _userRepository.All()
            };
            _bus.Reply(response);
        }

        public void Handle(GetUserByIdQuery message)
        {
            var response = new GetUserByIdResponse
            {
                User = _userRepository.ById(message.Id)
            };
            _bus.Reply(response);
        }

        public void Handle(GetUserByEmailQuery message)
        {
            var response = new GetUserByEmailResponse
            {
                User = _userRepository.ByEmail(message.Email)
            };
            _bus.Reply(response);
        }

        public void Handle(GetUserByUsernameQuery message)
        {
            var response = new GetUserByUsernameResponse
            {
                User = _userRepository.ByUsername(message.Username)
            };
            _bus.Reply(response);
        }

        public void Handle(CreateUser message)
        {
            // TODO: Add NLog to ECFX.WebService
            var newUser = _userRepository.Create(message.User);
            _bus.Publish<UserCreated>(x => x.User = newUser);
        } 
    }
}