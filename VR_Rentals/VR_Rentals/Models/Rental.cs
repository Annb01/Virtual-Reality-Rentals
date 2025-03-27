namespace VR_Rentals.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int EquipmentId { get; set; }
        public VrEquipment VrEquipment { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime PlannedReturnDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public double TotalRentalCost { get; set; }
    }
}
