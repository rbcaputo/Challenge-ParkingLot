using ParkingLotAPI.Utils;
using System.ComponentModel.DataAnnotations;

namespace ParkingLotAPI.Models.Lot
{
	public class FareModel
	{
		private DateTime _startDate = DateTime.UtcNow;

		private DateTime? _endDate;

		private decimal _pricePerHour = 5.00m;

		[Key]
		public int Id { get; set; }

		public DateTime StartDate
		{
			get => _startDate;
			set
			{
				ValidatorClass.ValidateDuration(_startDate, value);

				_startDate = value;
			}
		}

		public DateTime? EndDate
		{
			get => _endDate;
			set
			{
				ValidatorClass.ValidateDuration(_startDate, value);

				_endDate = value;
			}
		}

		public decimal PricePerHour
		{
			get => _pricePerHour;
			set
			{
				ValidatorClass.ValidatePricePerHour(value);

				_pricePerHour = value;
			}
		}

		public bool IsCurrent => ValidatorClass.CheckIfFareIsCurrent(this);
	}
}
