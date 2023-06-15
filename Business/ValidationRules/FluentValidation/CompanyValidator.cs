using Entities.Concrete.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CompanyValidator : AbstractValidator<Company>
    {
        public CompanyValidator()
        {
            RuleFor(c => c.CompanyName).NotEmpty();
            RuleFor(c => c.CompanyName).Length(1, 100);
            RuleFor(c => c.CompanyName).Must(NotStartWith);
        }

        private bool NotStartWith(string arg)
        {
            return !arg.ToLower().StartsWith('ğ');
        }
    }
}

