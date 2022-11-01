using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                .FirstAsync( s=> s.Id == request.Id, cancellationToken);
            
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
