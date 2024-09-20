using ParkingLotAPI.Utils;
using System.ComponentModel.DataAnnotations;

namespace ParkingLotAPI.Models.Lot
{
	public class FareModel
	{
		private DateTime _startDate;

		private DateTime? _endDate;

		private decimal _pricePerHour;

		[Key]
		public int Id { get; private set; }

		public DateTime StartDate
		{
			get => _startDate;
			set
			{
				_startDate = value;
				ValidatorClass.ValidateFareDuration(this);
			}
		}

		public DateTime? EndDate
		{
			get => _endDate;
			set
			{
				_endDate = value;
				ValidatorClass.ValidateFareDuration(this);
			}
		}

		public decimal PricePerHour
		{
			get => _pricePerHour;
			set
			{
				_pricePerHour = value;
				ValidatorClass.ValidatePricePerHour(this);
			}
		}

		public ICollection<ParkingModel> Parkings { get; set; } = [];

		public bool IsCurrent { get; set; }
	}
}
