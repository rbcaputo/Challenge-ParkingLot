using Microsoft.AspNetCore.Mvc;
using ParkingLotAPI.Dtos.Lot.Get;
using ParkingLotAPI.Dtos.Lot.PostPut;
using ParkingLotAPI.Interfaces.Lot.Requests;
using static ParkingLotAPI.Data.Constants.SizeFareMods;

namespace ParkingLotAPI.Controllers.Lot
{
	[Route("[controller]")]
	[ApiController]
	public class VehicleController(IVehicleService service) : ControllerBase
	{
		private readonly IVehicleService _service = service;

		[HttpGet]
		public async Task<ActionResult<ICollection<VehicleGetDto>>> GetAllVehiclesAsync()
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<VehicleGetDto> vehicles = await _service.GetAllVehiclesAsync(cancellation);

				return vehicles.Count == 0
					? NotFound("No vehicles were found.")
					: Ok(vehicles);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("parked")]
		public async Task<ActionResult<ICollection<VehicleGetDto>>> GetAllParkedVehicles()
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<VehicleGetDto> vehicles = await _service.GetAllParkedVehiclesAsync(cancellation);

				return vehicles.Count == 0
					? NotFound("No parked vehicles were found.")
					: Ok(vehicles);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpGet("size/{size}")]
		public async Task<ActionResult<ICollection<VehicleGetDto>>> GetAllVehiclesBySizeAsync(VehicleSize size)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				ICollection<VehicleGetDto> vehicles = await _service.GetAllVehiclesBySizeAsync(size, cancellation);

				return vehicles.Count == 0
					? NotFound("No vehicles were found with given size.")
					: Ok(vehicles);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[HttpGet("licensePlate/{licensePlate}")]
		public async Task<ActionResult<VehicleGetDto>> GetVehicleByLicensePlateAsync(string licensePlate)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				VehicleGetDto? vehicle = await _service.GetVehicleByLicensePlateAsync(licensePlate, cancellation);

				return vehicle == null
					? NotFound("No vehicle was found with the given license plate.")
					: Ok(vehicle);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> AddVehicleAsync(VehiclePostPutDto vehicleDto)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool isAdded = await _service.AddVehicleAsync(vehicleDto, cancellation);

				return !isAdded
					? StatusCode(500, "New vehicle could not be added.")
					: Ok("New vehicle added successfully.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateVehicleByLicensePlateAsync(VehiclePostPutDto vehicleDto)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isUpdated = await _service.UpdateVehicleByLicensePlateAsync(vehicleDto, cancellation);

				return isUpdated == null
					? NotFound("No vehicle was found with the given license plate.")
				: (bool)!isUpdated
					? StatusCode(500, "Vehicle could not be updated.")
					: Ok("Vehicle updated successfully.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveVehicleByLicensePlateAsync(string licensePlate)
		{
			try
			{
				CancellationToken cancellation = HttpContext.RequestAborted;
				bool? isRemoved = await _service.RemoveVehicleByLicensePlateAsync(licensePlate, cancellation);

				return isRemoved == null
					? NotFound("No vehicle was found with the given license plate.")
				: (bool)!isRemoved
					? StatusCode(500, "Vehicle could not be removed.")
					: Ok("Vehicle removed successfully.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
