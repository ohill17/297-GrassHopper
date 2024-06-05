using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
	public class PhotoGroup
	{
		[Key]
		public int GroupId { get; set; }
		public string GroupName { get; set; } = null!;
		public string GroupDescription { get; set; } = null!;
		public bool IsHidden { get; set; } = false;
		public List<Photo> Photos { get; set; } = new();
		public List<Tag> GroupTags { get; set; } = new();
		public List<Portfolio> AssocPortfolios { get; set; } = new();
	}
}
