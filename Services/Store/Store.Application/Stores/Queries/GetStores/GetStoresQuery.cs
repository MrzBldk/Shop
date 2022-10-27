using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Common.Interfaces;

namespace Store.Application.Stores.Queries.GetStores
{
    public record GetStoresQuery : IRequest<StoreViewModel>;

    public class GetStoresQueryHandler : IRequestHandler<GetStoresQuery, StoreViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetStoresQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StoreViewModel> Handle(GetStoresQuery request, CancellationToken cancellationToken)
        {
            return new()
            {
                Stores = await _context.Stores
                .AsNoTracking()
                .ProjectTo<StoreDTO>(_mapper.ConfigurationProvider)
                .OrderBy(s => s.Name)
                .ToListAsync(cancellationToken)
            };
        }
    }
}
