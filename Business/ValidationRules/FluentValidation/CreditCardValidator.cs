using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CreditCardValidator:AbstractValidator<CreditCardInformation>
    {
        public CreditCardValidator()
        {
            RuleFor(c => c.CardNumber).NotEmpty();
            RuleFor(c => c.CardMonth).NotEmpty();
            RuleFor(c => c.CardYear).NotEmpty();
            RuleFor(c => c.CardSecurityNumber).NotEmpty();
            RuleFor(c => c.CardName).NotEmpty();

            RuleFor(c => c.CardNumber).Must(x => x.Length == 16).WithMessage("Credit card number must be 16 characters");
            RuleFor(c => c.CardMonth).GreaterThan(0).LessThan(13);
            RuleFor(c => c.CardMonth).GreaterThan(DateTime.Now.AddMonths(-1).Month);
            RuleFor(c => c.CardYear).GreaterThan(DateTime.Now.AddYears(-1).Year);
            RuleFor(c => c.CardSecurityNumber).Must(x => x.ToString().Length == 3);
        }

    }
}
