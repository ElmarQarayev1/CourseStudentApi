using System;
using FluentValidation;

namespace CourseApi.Dtos.CourseDtos
{
	public class CourseCreateDto
	{
		public string Name { get; set; }

        public byte Limit { get; set; }

     
    }

    public class CourseCreateDtoValidator : AbstractValidator<CourseCreateDto>
    {
        public CourseCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(20).MinimumLength(4);

            RuleFor(x => (int)x.Limit).NotNull().InclusiveBetween(5, 20);
        }
    }
}

