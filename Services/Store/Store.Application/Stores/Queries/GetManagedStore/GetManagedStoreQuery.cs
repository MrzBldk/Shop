using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;
using Store.Application.Stores.Queries.GetStore;

namespace Store.Application.Stores.Queries.GetManagedStore
{
    public record GetManagedStoreQuery : IRequest<StoreViewModel>;

    public class GetManagedStoreQueryHandler : IRequestHandler<GetManagedStoreQuery, StoreViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetManagedStoreQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<StoreViewModel> Handle(GetManagedStoreQuery request, CancellationToken cancellationToken)
        {

            var userId = Guid.Parse(_currentUserService.UserId);

            var entity = await _context.Stores
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);

            if (entity is null)
            {
                throw new NotFoundException();
            }

            var dto = _mapper.Map<StoreDTO>(entity);

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
