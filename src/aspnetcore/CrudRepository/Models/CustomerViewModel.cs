using FluentValidation;

namespace CrudRepository.Models
{
    public class CustomerViewModel : Customer
    {

    }

    public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerViewModelValidator()
        {
            RuleFor(x => x.Firstname).NotNull().Length(1, 10);
            RuleFor(x => x.Lastname).NotNull().Length(1, 10);
            RuleFor(x => x.Email).NotNull().EmailAddress();
        }
    }
}