﻿using System.ComponentModel.DataAnnotations;

namespace BANSACH.Models
{
	public class NhaCungCap
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Address { get; set; }
		public int PhoneNumber { get; set; }
	}
}
