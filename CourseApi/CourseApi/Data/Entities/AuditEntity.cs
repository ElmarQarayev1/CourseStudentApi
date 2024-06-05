﻿using System;
namespace CourseApi.Data.Entities
{
	public class AuditEntity:BaseEntity
	{
		public DateTime CreateAt { get; set; } = DateTime.Now;
		public DateTime ModifiedAt { get; set; }
		public bool IsDeleted { get; set; }
	}
}

