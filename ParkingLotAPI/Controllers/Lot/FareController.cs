using Microsoft.AspNetCore.Mvc;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.HttpRequests;

namespace ParkingLotAPI.Controllers.Lot
{
	[Route("api/[controller]")]
	[ApiController]
	public class FareController(IFareService service) : ControllerBase
	{
		private readonly IFareService _service = service;

		public async Task<ActionResult<ICollection<FareGetDto>>> GetAllFaresAsync()
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<FareGetDto> fares = await _service.GetAllFaresAsync(cancellation);

				return fares.Count == 0 ?
					NotFound("No fares were found.") :
					Ok(fares);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		public async Task<ActionResult<ICollection<FareGetDto>>> GetAllFaresByPricePerHourAsync(decimal pricePerHour)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<FareGetDto> fares = await _service.GetAllFaresByPricePerHourAsync(pricePerHour, cancellation);

				return fares.Count == 0 ?
					NotFound("No fares were found with given price per hour.") :
					Ok(fares);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		public async Task<ActionResult<FareGetDto>> GetFareByStartDateAsync(DateTime startDate)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				FareGetDto? fare = await _service.GetFareByStartDateAsync(startDate, cancellation);

				return fare == null ?
					NotFound("Fare not found with given start date.") :
					Ok(fare);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		public async Task<ActionResult<FareGetDto>> GetFareByEndDateAsync(DateTime endDate)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				FareGetDto? fare = await _service.GetFareByEndDateAsync(endDate, cancellation);

				return fare == null ?
					NotFound("Fare not found with given end date.") :
					Ok(fare);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		public async Task<IActionResult> AddFareAsync(FarePostPutDto fareDto)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool isAdded = await _service.AddFareAsync(fareDto, cancellation);

				return !isAdded ?
					BadRequest("New fare could not be added.") :
					Ok("New fare added successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		public async Task<IActionResult> UpdateFareByStartDateAsync(FarePostPutDto fareDto)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isUpdated = await _service.UpdateFareByStartDateAsync(fareDto, cancellation);

				return isUpdated == null ?
					NotFound("No fare was found with the given start date.") :
				(bool)!isUpdated ?
					BadRequest("Fare could not be updated.") :
					Ok("Fare updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		public async Task<IActionResult> RemoveFareByStartDateAsync(DateTime startDate)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isRemoved = await _service.RemoveFareByStartDateAsync(startDate, cancellation);

				return isRemoved == null ?
					NotFound("No fare was found with the given start date.") :
				(bool)!isRemoved ?
					BadRequest("Fare could not be removed.") :
					Ok("Fare removed successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
