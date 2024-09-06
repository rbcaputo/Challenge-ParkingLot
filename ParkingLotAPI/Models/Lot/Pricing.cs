namespace ParkingLotAPI.Models.Lot
{
	public class Pricing
	{
		private double _pricePerHour;
		private DateTime _startDate;
		private DateTime? _endDate;
		
		public int Id { get; set; }		
		public DateTime StartDate
		{
			get => _startDate;
			set
			{
				if (_endDate.HasValue &&
						value > _endDate.Value)
					throw new ArgumentException("StartDate cannot be after EndDate.");

				_startDate = value;
			}
		}
		public DateTime? EndDate
		{
			get => _endDate;
			set
			{
				if (value.HasValue &&
						value < _startDate)
					throw new ArgumentException("EndDate cannot be before StartDate.");

				_endDate = value;
			}
		}
		public double PricePerHour
		{
			get => _pricePerHour;
			set
			{
				if (value < 0)
					throw new ArgumentException("Price cannot be negative.");

				_pricePerHour = value;
			}
		}
	}
}
