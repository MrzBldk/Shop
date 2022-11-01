using MediatR;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UpdateStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateStoreCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Store entity = await _context.Stores
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(Domain.Entities.Store), request.Id);

            entity.Name = request.Name;
            entity.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
