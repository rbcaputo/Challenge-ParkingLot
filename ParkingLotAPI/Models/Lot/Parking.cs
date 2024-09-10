using ParkingLotAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotAPI.Models.Lot
{
	public class Parking
	{
		private DateTime? _exitTime;

		[Key]
		public int Id { get; set; }

		public Fare Fare { get; set; } = new();

		[ForeignKey(nameof(Fare))]
		public int FareId { get; set; }

		public Vehicle Vehicle { get; set; } = new();

		[ForeignKey(nameof(Parking))]
		public int VehicleId { get; set; }

		public DateTime EntryTime { get; set; } = DateTime.Now;

		public DateTime? ExitTime
		{
			get => _exitTime;
			set
			{
				FareValidator.ValidateExitTime(EntryTime, value);

				_exitTime = value;
			}
		}

		public TimeSpan Duration => (ExitTime ?? DateTime.Now) - EntryTime;

		public decimal TotalPrice => FareValidator.CalculateTotalPrice(this);
	}
}
