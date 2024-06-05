using System;
namespace CourseApi.Dtos.StudentDtos
{
	public class StudentDetailsDto
	{
        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        public string CourseName { get; set; }
    }
}

