using System.ComponentModel.DataAnnotations;

namespace GrassHopper.Models
{
	public class PhotoGroupModel
	{
		[Key]
		public int GroupId { get; set; }
		public string GroupName { get; set; } = null!;
		public string GroupDescription { get; set; } = null!;
		public List<Photo> Photos { get; set; } = new();
	}
}
