using FluentValidation;

namespace Infrastructure.Commands
{
    public interface ICommandValidatorFactory
    {
        IValidator<T>[] GetValidatorsForCommand<T>(T command);
    }
}
