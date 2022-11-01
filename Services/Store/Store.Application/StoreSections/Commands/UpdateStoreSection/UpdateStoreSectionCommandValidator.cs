using FluentValidation;

namespace Store.Application.StoreSections.Commands.UpdateStoreSection
{
    public class UpdateStoreSectionCommandValidator : AbstractValidator<UpdateStoreSectionCommand>
    {
        public UpdateStoreSectionCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(v => v.Description)
                .MaximumLength(200);
        }
    }
}
