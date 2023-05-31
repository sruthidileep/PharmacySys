using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PharmacySys.Manager;
using PharmacySys.Models;

[ApiController]
[Route("api/[controller]")]
public class PharmacyController : ControllerBase
{
    private readonly IPharmacyManager _pharmacyManager;

    public PharmacyController(IPharmacyManager pharmacyManager)
    {
        _pharmacyManager = pharmacyManager;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pharmacy>> GetPharmacyById(int id)
    {
        var pharmacy = await _pharmacyManager.GetPharmacyById(id);
        if (pharmacy == null)
        {
            return NotFound();
        }

        return pharmacy;
    }

    [HttpGet]
    public async Task<ActionResult<List<Pharmacy>>> GetAllPharmacies()
    {
        var pharmacies = await _pharmacyManager.GetAllPharmacies();
        return pharmacies;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePharmacy(int id, Pharmacy pharmacy)
    {
        if (id != pharmacy.Id)
        {
            return BadRequest();
        }

        bool updated = await _pharmacyManager.UpdatePharmacy(pharmacy);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("delivery-details")]
    public IActionResult GetDeliveryDetails()
    {
        var deliveryDetails = _pharmacyManager.GetDeliveryDetails();
        return Ok(deliveryDetails);
    }

    [HttpGet("warehouse-profit")]
    public IActionResult GetWarehouseProfitReport()
    {
        var report = _pharmacyManager.GetWarehouseProfitReport();
        return Ok(report);
    }

    [HttpGet("pharmacist-production")]
    public IActionResult GetPharmacistProductionReport()
    {
        var report = _pharmacyManager.GetPharmacistProductionReport();
        return Ok(report);
    }

}