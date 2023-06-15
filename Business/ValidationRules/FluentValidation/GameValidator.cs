using Entities.Concrete.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator()
        {
            RuleFor(c => c.GameName).NotEmpty();
            RuleFor(c => c.GameName).Length(1, 100);
            RuleFor(c => c.GameName).Must(NotStartWith);
            RuleFor(c => c.Price).NotEmpty();
            RuleFor(c => c.Price).LessThan(100);
            RuleFor(c => c.SystemRequirements).Length(1, 200);


        }

        private bool NotStartWith(string arg)
        {
            return !arg.ToLower().StartsWith('ğ');
        }
    }
}
