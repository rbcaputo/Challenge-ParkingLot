using Microsoft.AspNetCore.Mvc;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.HttpRequests;

namespace ParkingLotAPI.Controllers.Lot
{
	[Route("[controller]")]
	[ApiController]
	public class VehicleController(IVehicleService service) : ControllerBase
	{
		private readonly IVehicleService _service = service;

		public async Task<ActionResult<ICollection<VehicleGetDto>>> GetAllVehiclesAsync()
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<VehicleGetDto> vehicles = await _service.GetAllVehiclesAsync(cancellation);

				return vehicles.Count == 0 ?
					NotFound("No vehicles were found.") :
					Ok(vehicles);
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

		public async Task<ActionResult<ICollection<VehicleGetDto>>> GetAllParkedVehicles()
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<VehicleGetDto> vehicles = await _service.GetAllParkedVehiclesAsync(cancellation);

				return vehicles.Count == 0 ?
					NotFound("No parked vehicles were found.") :
					Ok(vehicles);
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

		public async Task<ActionResult<VehicleGetDto>> GetVehicleByLicensePlateAsync(string licensePlate)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				VehicleGetDto? vehicle = await _service.GetVehicleByLicensePlateAsync(licensePlate, cancellation);

				return vehicle == null ?
					NotFound("No vehicle was found with the given license plate.") :
					Ok(vehicle);
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

		public async Task<IActionResult> AddVehicleAsync(VehiclePostPutDto vehicleDto)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool isAdded = await _service.AddVehicleAsync(vehicleDto, cancellation);

				return !isAdded ?
					BadRequest("New vehicle could not be added.") :
					Ok("New vehicle added successfully.");
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

		public async Task<IActionResult> UpdateVehicleByLicensePlateAsync(VehiclePostPutDto vehicleDto)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isUpdated = await _service.UpdateVehicleByLicensePlateAsync(vehicleDto, cancellation);

				return isUpdated == null ?
				 NotFound("No vehicle was found with the given license plate.") :
			 (bool)!isUpdated ?
				 BadRequest("Vehicle could not be updated.") :
				 Ok("Vehicle updated successfully.");
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}

		public async Task<IActionResult> RemoveVehicleByLicensePlateAsync(string licensePlate)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isRemoved = await _service.RemoveVehicleByLicensePlateAsync(licensePlate, cancellation);

				return isRemoved == null ?
					NotFound("No vehicle was found with the given license plate.") :
				(bool)!isRemoved ?
					BadRequest("Vehicle could not be removed.") :
					Ok("Vehicle removed successfully.");
			}
			catch (Exception e)
			{
				return StatusCode(500, e.Message);
			}
		}
	}
}
