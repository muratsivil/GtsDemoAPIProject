using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreatedUserResponse>
{
    public required string Name { get; set; }
    public required string Surname { get; set; }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserCommand> _validator = new CreateUserCommandValidator();

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                // Log the validation errors
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                return null;
            }
            User user = _mapper.Map<User>(request);
            user.Id = Guid.NewGuid();
            await _userRepository.AddAsync(user);
            CreatedUserResponse response = _mapper.Map<CreatedUserResponse>(user);
            return response;
        }
    }
}
