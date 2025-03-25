using FluentValidation;
using Smarty.Notes.Infrastructure.Options;

namespace Smarty.Notes.Infrastructure.Validation;

public class EventBusOptionsValidator : AbstractValidator<EventBusOptions>
{
    public EventBusOptionsValidator()
    {
        RuleFor(f=>f.UserName).NotNull();
        RuleFor(f=>f.Password).NotNull();
        RuleFor(f=>f.HostName).NotNull();
    }

}
