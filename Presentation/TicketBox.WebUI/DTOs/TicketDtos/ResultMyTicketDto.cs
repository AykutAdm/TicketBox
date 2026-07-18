namespace TicketBox.WebUI.DTOs.TicketDtos
{
    public class ResultMyTicketDto
    {
        public int TicketId { get; set; }
        public string PNR { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }

        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string? ImageUrl { get; set; }
    }
}
