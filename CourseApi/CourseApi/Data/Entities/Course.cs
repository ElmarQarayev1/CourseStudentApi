using System;
namespace CourseApi.Data.Entities
{
	public class Course:AuditEntity
	{
		

		public string Name { get; set; }

		public byte Limit { get; set; }

		public List<Student> Students { get; set; }

		
	}
}

