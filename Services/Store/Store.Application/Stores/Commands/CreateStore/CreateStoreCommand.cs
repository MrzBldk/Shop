using MediatR;
using Store.Application.Common.Interfaces;

namespace Store.Application.Stores.Commands.CreateStore
{
    public record CreateStoreCommand : IRequest<Guid>
    {
        public string Name { get; init; }
        public string? Description { get; init; }
    }

    public class CreateStoreCommandHandler : IRequestHandler<CreateStoreCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Store entity = new()
            {
                Name = request.Name,
                Description = request.Description
            };

            _context.Stores.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
