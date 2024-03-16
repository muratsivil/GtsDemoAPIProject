using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Members.Queries.GetById;

public class GetByIdMemberQuery : IRequest<GetByIdMemberResponse>
{
    public Guid Id { get; set; }

    public class GetByIdMemberQueryHandler : IRequestHandler<GetByIdMemberQuery, GetByIdMemberResponse>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public GetByIdMemberQueryHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<GetByIdMemberResponse> Handle(GetByIdMemberQuery request, CancellationToken cancellationToken)
        {
            Member? member = await _memberRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            GetByIdMemberResponse response = _mapper.Map<GetByIdMemberResponse>(member);
            return response;
        }
    }
}
