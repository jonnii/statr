using FluentValidation;
using Statr.Configuration;

namespace Statr.Server.Configuration
{
    public class ConfigValidator : AbstractValidator<Config>
    {
        public ConfigValidator()
        {
            RuleFor(r => r.Entries).SetCollectionValidator(new EntryValidator());
        }
    }
}
