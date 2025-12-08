namespace MAKHAZIN.Core.DTOs
{
    public class BidDTO
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public decimal BidPrice { get; set; }
        public DateTime TimeStamp { get; set; }
        public string BidderName { get; set; }
    }
}
