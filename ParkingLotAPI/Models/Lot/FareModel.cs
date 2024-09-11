using ParkingLotAPI.Utils;
using System.ComponentModel.DataAnnotations;

namespace ParkingLotAPI.Models.Lot
{
	public class FareModel
	{
		private DateTime _startDate = DateTime.Now;

		private DateTime? _endDate;

		private decimal _pricePerHour = 1.00m;

		[Key]
		public int Id { get; set; }

		public DateTime StartDate
		{
			get => _startDate;
			set
			{
				FareValidator.ValidateDuration(_startDate, value);

				_startDate = value;
			}
		}

		public DateTime? EndDate
		{
			get => _endDate;
			set
			{
				FareValidator.ValidateDuration(_startDate, value);

				_endDate = value;
			}
		}

		public decimal PricePerHour
		{
			get => _pricePerHour;
			set
			{
				FareValidator.ValidatePricePerHour(value);

				_pricePerHour = value;
			}
		}

		public bool IsCurrent => (StartDate <= DateTime.Now) &&
														 (!_endDate.HasValue || EndDate >= DateTime.Now);
	}
}
