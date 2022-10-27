using FluentValidation;

namespace Store.Application.StoreSections.Commands.CreateStoreSection
{
    public class CreateStoreSectionCommandValidator : AbstractValidator<CreateStoreSectionCommand>
    {
        public CreateStoreSectionCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();

            RuleFor(v => v.Description)
                .MaximumLength(200);
        }
    }
}
