using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacySys.Models;
using PharmacySys.Repository;

public interface IPharmacyRepository
{
    Task<Pharmacy> GetPharmacyById(int id);
    Task<List<Pharmacy>> GetAllPharmacies();
    Task<bool> UpdatePharmacy(Pharmacy pharmacy);
    IEnumerable<Delivery> GetDeliveryDetails();

    List<WarehouseProfit> GetWarehouseProfitReport();
    List<Pharmacists> GetPharmacistProductionReport();

}
public class PharmacyRepository : IPharmacyRepository
{
    private readonly PharmacyDbContext _dbContext;

    public PharmacyRepository(PharmacyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Pharmacy> GetPharmacyById(int id)
    {
        return await _dbContext.Pharmacy.FindAsync(id);
    }

    public async Task<List<Pharmacy>> GetAllPharmacies()
    {

        var pharmacy = _dbContext.Pharmacy
                    .FromSql($"SELECT * FROM dbo.Pharmacy")
                    .ToList();

        //var list = await _dbContext.Pharmacy.ToListAsync();
        return pharmacy; 
    }

    public async Task<bool> UpdatePharmacy(Pharmacy pharmacy)
    {
        var existingPharmacy = await _dbContext.Pharmacy.FindAsync(pharmacy.Id);

        if (existingPharmacy == null)
        {
            return false;
        }

        //assigning value if pharamcy.value is not null
        existingPharmacy.Name = pharmacy.Name ?? existingPharmacy.Name;
        existingPharmacy.Address = pharmacy.Address ?? existingPharmacy.Address;
        existingPharmacy.City = pharmacy.City ?? existingPharmacy.City;
        existingPharmacy.State = pharmacy.State ?? existingPharmacy.State;
        existingPharmacy.Zip = pharmacy.Zip ?? existingPharmacy.Zip;
        existingPharmacy.NumberOfFilledPrescriptions = pharmacy.NumberOfFilledPrescriptions ?? existingPharmacy.NumberOfFilledPrescriptions;
        existingPharmacy.UpdatedDate = DateTime.Now;

        int rowsAffected = await _dbContext.SaveChangesAsync();

        return rowsAffected > 0;
    }

    public IEnumerable<Delivery> GetDeliveryDetails()
    {
        var query = @"SELECT
                    d.DeliveryID,
                    w.Name AS WarehouseFrom,
                    p.Name AS PharmacyTo,
                    d.DrugName,
                    d.UnitCount,
                    d.UnitPrice,
                    d.TotalPrice,
                    d.DeliveryDate
                FROM
                    Deliveries d
                JOIN
                    Warehouses w ON d.WarehouseID = w.WarehouseID
                JOIN
                    Pharmacy p ON d.PharmacyID = p.ID";

        return _dbContext.Deliveries.FromSqlRaw(query);
    }

    public List<WarehouseProfit> GetWarehouseProfitReport()
    {
        var query = @"
    SELECT
        w.WarehouseID,
        w.Name AS Warehouse,
        SUM(d.TotalPrice) AS TotalRevenue,
        SUM(d.UnitCount) AS TotalUnitCount,
        SUM(d.TotalPrice) / SUM(d.UnitCount) AS AverageProfit
    FROM
        Warehouses w
    LEFT JOIN
        Deliveries d ON d.WarehouseID = w.WarehouseID
    GROUP BY
        w.WarehouseID, w.Name
    ORDER BY
        SUM(d.TotalPrice) DESC";

        return _dbContext.Warehouses.FromSqlRaw(query).ToList();
    }

    public List<Pharmacists> GetPharmacistProductionReport()
    {
        var query = from pharmacist in _dbContext.Pharmacists
                    join delivery in _dbContext.Deliveries on pharmacist.PrimaryDrug equals delivery.DrugName
                    join pharmacy in _dbContext.Pharmacy on delivery.PharmacyId equals pharmacy.Id
                    group new { pharmacist, delivery } by new { pharmacist.PharmacistID, pharmacy.Name, pharmacist.PrimaryDrug } into g
                    select new Pharmacists
                    {
                        PharmacistID = g.Key.PharmacistID,
                        Name = g.Key.Name,
                        PrimaryDrug = g.Key.PrimaryDrug,
                        TotalUnitCountPrimaryDrug = g.Sum(x => x.delivery.DrugName == g.Key.PrimaryDrug ? x.delivery.UnitCount : 0),
                        TotalUnitCountOtherDrugs = g.Sum(x => x.delivery.DrugName != g.Key.PrimaryDrug ? x.delivery.UnitCount : 0)
                    };

        return query.ToList();
    }
}