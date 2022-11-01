using MediatR;
using Store.Application.Common.Exceptions;
using Store.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Stores.Commands.DeleteStore
{
    public record DeleteStoreCommand(Guid Id) : IRequest;

    public class DeleteStoreCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public DeleteStoreCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStoreCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Store entity = await _context.Stores
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity is null)
                throw new NotFoundException(nameof(Domain.Entities.Store), request.Id);

            _context.Stores.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
