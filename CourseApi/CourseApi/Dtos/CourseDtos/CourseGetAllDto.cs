using System;
namespace CourseApi.Dtos.CourseDtos
{
	public class CourseGetAllDto
	{
		public string Name { get; set; }

		public byte Limit { get; set; }

		public int StudentCount { get; set; }
	}
}

