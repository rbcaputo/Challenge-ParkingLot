using Microsoft.AspNetCore.Mvc;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.Requests;

namespace ParkingLotAPI.Controllers.Lot
{
	[Route("[controller]")]
	[ApiController]
	public class FareController(IFareService service) : ControllerBase
	{
		private readonly IFareService _service = service;

		[HttpGet]
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

		[HttpGet("pricePerHour")]
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

		[HttpGet("startDate")]
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

		[HttpGet("endDate")]
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

		[HttpGet("current")]
		public async Task<ActionResult<FareGetDto>> GetCurrentFareDtoAsync()
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				FareGetDto? fare = await _service.GetCurrentFareDtoAsync(cancellation);

				return fare == null ?
					NotFound("No current fare was found.") :
					Ok(fare);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
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

		[HttpPut]
		public async Task<IActionResult> UpdateCurrentFareAsync(FarePostPutDto fareDto)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isUpdated = await _service.UpdateCurrentFareAsync(fareDto, cancellation);

				return isUpdated == null ?
					NotFound("No current fare was found.") :
				(bool)!isUpdated ?
					BadRequest("Current fare could not be updated.") :
					Ok("Current fare updated successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveFareByStartDateAsync(DateTime startDate)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isRemoved = await _service.RemoveFareByStartDateAsync(startDate, cancellation);

				return isRemoved == null ?
					NotFound("No fare was found with given start date.") :
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
