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

            RuleFor(c => c.CardNumber).MaximumLength(16);
            RuleFor(c => c.CardYear).GreaterThan(DateTime.Now.AddYears(-1).Year);
        }
    }
}
