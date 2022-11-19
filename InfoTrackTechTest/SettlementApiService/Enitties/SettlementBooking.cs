using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SettlementApiService.Enitties
{
    [Table("SettlementBooking")]
    public class SettlementBooking
    {
        [Key]
        public Guid SettlementBookingId { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime BookingTimeFrom { get; set; }
        public DateTime BookingTimeTo { get; set; }

    }
}
