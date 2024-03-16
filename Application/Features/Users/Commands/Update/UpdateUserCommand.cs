using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>{
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateUserCommand> _validator = new UpdateUserCommandValidator();

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IValidator<UpdateUserCommand> validator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                // Log the validation errors
                foreach (var error in validationResult.Errors)
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                return null;
            }
            User? user = await _userRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            user = _mapper.Map(request, user);
            
            await _userRepository.UpdateAsync(user);
            UpdatedUserResponse response = _mapper.Map<UpdatedUserResponse>(user);
            return response;
        }
    }
}
