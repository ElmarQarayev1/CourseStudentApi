using System;
namespace CourseApi.Data.Entities
{
	public class Course
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime CreateAt { get; set; }

		public DateTime ModifiedAt { get; set; }
	}
}

