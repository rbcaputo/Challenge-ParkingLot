using ParkingLotAPI.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLotAPI.Models.Lot
{
	public class Fare
	{
		private DateTime _startDate = DateTime.Now;

		private DateTime? _endDate;

		private decimal _pricePerHour = 0;

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

		public ICollection<Parking> Parkings { get; set; } = [];
	}
}
