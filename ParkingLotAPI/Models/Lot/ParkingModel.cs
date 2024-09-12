using ParkingLotAPI.Interfaces.Lot.Requests;
using ParkingLotAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotAPI.Models.Lot
{
	public class ParkingModel
	{
		private DateTime? _exitTime;

		[Key]
		public int Id { get; set; }

		public FareModel Fare { get; set; } = new();

		[ForeignKey(nameof(Fare))]
		public int FareId { get; set; }

		public VehicleModel Vehicle { get; set; } = new();

		[ForeignKey(nameof(ParkingModel))]
		public int VehicleId { get; set; }

		public DateTime EntryTime { get; set; } = DateTime.UtcNow;

		public DateTime? ExitTime
		{
			get => _exitTime;
			set
			{
				ValidatorClass.ValidateExitTime(EntryTime, value);
				_exitTime = value;
			}
		}

		public TimeSpan Duration => (ExitTime ?? DateTime.UtcNow) - EntryTime;

		public decimal TotalPrice => CalculatorClass.CalculateTotalPrice(this);

		public async Task SetCurrentFareAsync(IFareService service, CancellationToken cancellation)
		{
			try
			{
				FareModel? fare = await service.GetCurrentFareModelAsync(cancellation);

				if (fare == null)
					throw new InvalidOperationException($"{nameof(fare)} cannot be null.");

				Fare = fare;
				FareId = fare.Id;
			}
			catch
			{
				throw;
			}
		}
	}
}
