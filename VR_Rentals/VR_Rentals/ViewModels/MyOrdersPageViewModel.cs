using VR_Rentals.Models;

namespace VR_Rentals.ViewModels
{
    public class MyOrdersPageViewModel
    {
        public DateTime RentalDate { get; set; }
        public DateTime PlannedReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double TotalRentalCost { get; set; }
        public string EquipmentName { get; set; }
        public TypeEnum TypeName { get; set; }
        public string Manufacturer { get; set; }
        public double PricePerDay { get; set; }
        public byte[] Image { get; set; }
    }
}
