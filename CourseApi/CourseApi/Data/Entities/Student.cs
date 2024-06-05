using System;
namespace CourseApi.Data.Entities
{
	public class Student:AuditEntity
	{
		public string FullName { get; set; }

		public string Email { get; set; }

		public DateTime BirthDate { get; set; }

		public Course Course { get; set; }

		public int CourseId { get; set; }

	}
}

