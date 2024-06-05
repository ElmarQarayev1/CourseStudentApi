using System;
namespace CourseApi.Dtos.StudentDtos
{
	public class StudentUpdateDto
	{
        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public int CourseId { get; set; }
    }
}

