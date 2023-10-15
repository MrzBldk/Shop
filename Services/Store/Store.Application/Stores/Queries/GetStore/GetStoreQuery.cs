using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;

namespace Store.Application.Stores.Queries.GetStore
{
    public record GetStoreQuery : IRequest<StoreViewModel>
    {
        public Guid Id { get; init; }
    }

    public class GetStoreQueryHandler : IRequestHandler<GetStoreQuery, StoreViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStoreQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StoreViewModel> Handle(GetStoreQuery request, CancellationToken cancellationToken)
        {
            var dto = await _context.Stores
                .AsNoTracking()
                .ProjectTo<StoreDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync( s=> s.Id == request.Id, cancellationToken);

            if (dto is null)
            {
                throw new NotFoundException();
            }

            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Sections = dto.Sections
            };
        }
    }
}
