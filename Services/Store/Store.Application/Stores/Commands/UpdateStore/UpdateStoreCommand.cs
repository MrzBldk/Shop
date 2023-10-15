using MediatR;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;

namespace Store.Application.Stores.Commands.UpdateStore
{
    public record UpdateStoreCommand : IRequest
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
    }

    public class UpdateStoreCommandHandler : IRequestHandler<UpdateStoreCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateStoreCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Store entity = await _context.Stores
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(Domain.Entities.Store), request.Id);

            if (entity.UserId != Guid.Parse(_currentUserService.UserId))
                throw new ForbiddenException(nameof(Domain.Entities.Store), request.Id);

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
