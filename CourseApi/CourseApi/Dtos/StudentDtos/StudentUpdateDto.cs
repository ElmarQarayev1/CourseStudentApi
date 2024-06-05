﻿using System;
using FluentValidation;

namespace CourseApi.Dtos.StudentDtos
{
    public class StudentUpdateDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public int CourseId { get; set; }
    }
    public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
    {
        public StudentUpdateDtoValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().MaximumLength(35).MinimumLength(6);

            RuleFor(x => x.Email).NotNull().EmailAddress();

            RuleFor(x => x.Birthdate).NotEmpty().Must(BeAtLeast14YearsOld);

            RuleFor(x => x.CourseId).NotNull().GreaterThanOrEqualTo(0);

        }
        private bool BeAtLeast14YearsOld(DateTime birthdate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthdate.Year;

            if (birthdate.Date > today.AddYears(-age))
            {
                age--;
            }
            return age >= 14;
        }
    }
}

