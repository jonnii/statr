using FluentValidation;

namespace Statr.Configuration
{
    public class EntryValidator : AbstractValidator<Entry>
    {
        public EntryValidator()
        {
            RuleFor(e => e.Name).NotEmpty().NotNull();
            RuleFor(e => e.Pattern).NotEmpty().NotNull();
        }
    }
}