using System.ComponentModel.DataAnnotations;

namespace Electro_Project.Models
{
	public class Address
	{
		public int Id { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public int StreetNumber { get; set; }
		public int FloorNumber { get; set; }
	}
}