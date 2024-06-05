using System;
namespace CourseApi.Dtos.CourseDtos
{
	public class CourseDetailsDto
	{
		public string Name { get; set; }

        public byte Limit { get; set; }

		public int StudentCount { get; set; }
    }
}

