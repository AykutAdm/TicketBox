namespace TicketBox.WebUI.DTOs.EventDtos
{
    public class ResultEventDto
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsFeatured { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
