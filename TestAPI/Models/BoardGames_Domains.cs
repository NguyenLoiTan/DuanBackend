﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestAPI.Models
{
	[Table("BoardGames_Domains")]
	public class BoardGames_Domains
	{
		[Key]
		[Required]
		public int BoardGameId { get; set; }
		[Key]
		[Required]
		public int DomainId { get; set; }
		[Required]
		public DateTime CreatedDate { get; set; }

		public BoardGame? BoardGame { get; set; }
		public Domain? Domain { get; set; }
	}

	
}
