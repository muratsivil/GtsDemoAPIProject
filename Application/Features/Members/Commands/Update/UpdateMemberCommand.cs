using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using MediatR;

namespace Application.Features.Members.Commands.Update;

public class UpdateMemberCommand : IRequest<UpdatedMemberResponse>
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public Gender Gender { get; set; }

    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, UpdatedMemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateMemberCommand> _validator = new UpdateMemberCommandValidator();

        public UpdateMemberCommandHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedMemberResponse> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
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
            Member? member = await _memberRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            member = _mapper.Map(request, member);
            
            await _memberRepository.UpdateAsync(member);
            UpdatedMemberResponse response = _mapper.Map<UpdatedMemberResponse>(member);
            return response;
        }
    }
}
