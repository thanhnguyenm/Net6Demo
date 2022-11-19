namespace SettlementApiService.Models
{
    public class SettlementBookingModel
    {

        public string Name { get; set; }
        public string BookingTime { get; set; }
    }

    public class SettlementBookingModelResult
    {

        public Guid BookingId { get; set; }
        
    }
}
