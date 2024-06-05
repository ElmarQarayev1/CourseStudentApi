using System;
using CourseApi.Data.Entities;

namespace CourseApi.Dtos.StudentDtos
{
	public class StudentGetAllDto
	{
		public string FullName { get; set; }

		public string Email { get; set; }

		public DateTime Birthdate { get; set; }

		public string CourseName { get; set; }

	}
}

