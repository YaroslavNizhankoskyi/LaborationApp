using API.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Validators
{
    public class CreateFactorDtoValidator : AbstractValidator<CreateFactorDto>
    {
        public CreateFactorDtoValidator()
        {
            RuleFor(m => m.Coefficient)
                .NotEmpty()
                .LessThanOrEqualTo(20);

            RuleFor(m => m.Name)
                .NotEmpty()
                .MaximumLength(50);
        }
    }
}
