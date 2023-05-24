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
}