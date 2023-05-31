namespace PharmacySys.Models
{
    public class Delivery
    {
            public int DeliveryID { get; set; }
            public int PharmacyId { get; set; }
            public string WarehouseFrom { get; set; }
            public string PharmacyTo { get; set; }
            public string DrugName { get; set; }
            public int UnitCount { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalPrice { get; set; }
            public DateTime DeliveryDate { get; set; }
        
    }
}
