using PharmacySys.Models;
using PharmacySys.Repository;

namespace PharmacySys.Manager
{
    public interface IPharmacyManager
    {
        Task<Pharmacy> GetPharmacyById(int id);
        Task<List<Pharmacy>> GetAllPharmacies();
        Task<bool> UpdatePharmacy(Pharmacy pharmacy);
    }
    public class PharmacyManager : IPharmacyManager
    {
        private readonly IPharmacyRepository _pharmacyRepository;

        public PharmacyManager(IPharmacyRepository pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        public async Task<Pharmacy> GetPharmacyById(int id)
        {
            return await _pharmacyRepository.GetPharmacyById(id);
        }

        public async Task<List<Pharmacy>> GetAllPharmacies()
        {
            return await _pharmacyRepository.GetAllPharmacies();
        }

        public async Task<bool> UpdatePharmacy(Pharmacy pharmacy)
        {
            return await _pharmacyRepository.UpdatePharmacy(pharmacy);
        }
    }
}
