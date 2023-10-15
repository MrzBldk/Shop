using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Store.Application.Common.Interfaces;

namespace Store.Application.Stores.Commands.UpdateStore
{
    public class UpdateStoreCommandValidator : AbstractValidator<UpdateStoreCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateStoreCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .NotEmpty().WithName("Store name is required.")
                .MaximumLength(50).WithName("Store name must not exceed 50 characters.")
                .MustAsync(BeUniqueName).WithName("Store with specified name already exists.");

            RuleFor(v => v.Description)
                .MaximumLength(200).WithName("Store description must not exceed 200 characters.");
        }

        public async Task<bool> BeUniqueName(UpdateStoreCommand model, string name, CancellationToken cancellationToken)
        {
            return await _context.Stores
                .Where(l => l.Id != model.Id)
                .AllAsync(l => l.Name != name, cancellationToken);
        }
    }
}
