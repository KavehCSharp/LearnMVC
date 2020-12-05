using System;
using FluentValidation;

namespace CrudImageRepository.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool IsActive { get; set; }

        public int Age { get; set; }

        public string City { get; set; }
    }

    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(x => x.Firstname).NotNull().Length(1, 10);
            RuleFor(x => x.Lastname).NotNull().Length(1, 10);
            RuleFor(x => x.City).NotNull().Length(1, 10);
            RuleFor(x => x.Age).GreaterThan(1);
        }
    }
}