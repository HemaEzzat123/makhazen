namespace MAKHAZIN.Core.DTOs
{
    public class AuctionDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal StartingPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string CreatedBy { get; set; }

        public List<BidDTO> Bids { get; set; }
    }
}
