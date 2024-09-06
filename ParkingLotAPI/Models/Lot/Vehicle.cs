using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotAPI.Models.Lot
{
	public class Vehicle
	{
		private Dictionary<string, double> _size = [];

		public int Id { get; set; }
		public string Plate { get; set; } = string.Empty;
		public string Size
		{
			get => JsonConvert.SerializeObject(_size);
			set
			{
				var sizeData = JsonConvert.DeserializeObject<Dictionary<string, double>>(value);
				_size = sizeData ?? [];
			}
		}
		[NotMapped]
		public Dictionary<string, double> SizeData
		{
			get => _size;
			set => _size = value ?? [];
		}
		public string Brand { get; set; } = string.Empty;
		public string Model { get; set; } = string.Empty;
		public string Color { get; set; } = string.Empty;
	}
}
