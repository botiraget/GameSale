using Entities.Concrete.Models;
using Entities.Dtos.CategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty();
            RuleFor(c => c.CategoryName).Length(1, 100);
            RuleFor(c => c.CategoryName).Must(NotStartWith);
        }

        private bool NotStartWith(string arg)
        {
            return !arg.ToLower().StartsWith('ğ');
        }
    }
}
