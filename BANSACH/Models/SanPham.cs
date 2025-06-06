﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BANSACH.Models
{
	public class SanPham
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Price { get; set; }
		public string Description { get; set; }

		public string ImageUrl { get; set; }
		public string Author { get; set; }
		public int NhaCungCapId { get; set; }

		[ForeignKey("NhaCungCapId")]
		[ValidateNever]
		public NhaCungCap NhaCungCap { get; set; }

		public int TheLoaiId { get; set; }
		[ForeignKey("TheLoaiId")]
		[ValidateNever]
		public TheLoai TheLoai { get; set; }



	}
}
