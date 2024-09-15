using Microsoft.AspNetCore.Mvc;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.Requests;

namespace ParkingLotAPI.Controllers.Lot
{
	[Route("[controller]")]
	[ApiController]
	public class ParkingController(IParkingService service) : ControllerBase
	{
		private readonly IParkingService _service = service;

		[HttpGet]
		public async Task<ActionResult<ICollection<ParkingGetDto>>> GetAllParkingsAsync()
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<ParkingGetDto> parkings = await _service.GetAllParkingsAsync(cancellation);

				return parkings.Count == 0 ?
					NotFound("No parking sessions were found.") :
					Ok(parkings);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("pricePerHour")]
		public async Task<ActionResult<ICollection<ParkingGetDto>>> GetAllParkingsByPricePerHourAsync(decimal pricePerHour)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<ParkingGetDto> parkings = await _service.GetAllParkingsByPricePerHourAsync(pricePerHour, cancellation);

				return parkings.Count == 0 ?
					NotFound("No parking sessions were found with given price per hour.") :
					Ok(parkings);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("duration")]
		public async Task<ActionResult<ICollection<ParkingGetDto>>> GetAllParkingsByDurationAsync(TimeSpan duration)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<ParkingGetDto> parkings = await _service.GetAllParkingsByDurationAsync(duration, cancellation);

				return parkings.Count == 0 ?
					NotFound("No parking sessions were found with given duration.") :
					Ok(parkings);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("currentFare")]
		public async Task<ActionResult<ICollection<ParkingGetDto>>> GetAllParkingsByCurrentFareAsync()
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<ParkingGetDto> parkings = await _service.GetAllParkingsByCurrentFareAsync(cancellation);

				return parkings.Count == 0 ?
					NotFound("No parking sessions were found with current fare.") :
					Ok(parkings);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddParkingAsync(ParkingPostPutDto parkingDto)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool isAdded = await _service.AddParkingAsync(parkingDto, cancellation);

				return !isAdded ?
					StatusCode(500, "Parking session could not be added.") :
					Ok("Parking session added successfully.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCurrentParkingByLicensePlateAsync(ParkingPostPutDto parkingDto)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isUpdated = await _service.UpdateCurrentParkingByLicensePlateAsync(parkingDto, cancellation);

				return isUpdated == null ?
					NotFound("No current parking session was found with given license plate.") :
				(bool)!isUpdated ?
					StatusCode(500, "Current parking session could not be updated.") :
					Ok("Current parking session updated successfully.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveParkingByLicensePlateEntryTimeAsync(string licensePlate, DateTime entryTime)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isRemoved = await _service.RemoveParkingByLicensePlateEntryTimeAsync(licensePlate, entryTime, cancellation);

				return isRemoved == null ?
					NotFound("No parking session was found with given license plate and entry time.") :
					(bool)!isRemoved ?
					StatusCode(500, "Parking session could not be removed.") :
					Ok("Parking session removed successfully.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
