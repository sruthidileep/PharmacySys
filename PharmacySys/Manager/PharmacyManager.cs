using PharmacySys.Models;
using PharmacySys.Repository;

namespace PharmacySys.Manager
{
    public interface IPharmacyManager
    {
        Task<Pharmacy> GetPharmacyById(int id);
        Task<List<Pharmacy>> GetAllPharmacies();
        Task<bool> UpdatePharmacy(Pharmacy pharmacy);

        IEnumerable<Delivery> GetDeliveryDetails();

        IEnumerable<WarehouseProfit> GetWarehouseProfitReport();

        List<Pharmacists> GetPharmacistProductionReport();
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

        public IEnumerable<Delivery> GetDeliveryDetails()
        {
            return _pharmacyRepository.GetDeliveryDetails();
        }

        public IEnumerable<WarehouseProfit> GetWarehouseProfitReport()
        {
            return _pharmacyRepository.GetWarehouseProfitReport();
        }

        public List<Pharmacists> GetPharmacistProductionReport()
        {
            return _pharmacyRepository.GetPharmacistProductionReport();
        }
    }
}
