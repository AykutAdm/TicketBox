namespace TicketBox.WebUI.DTOs.UserDtos
{
    public class ResultUserDashboardDto
    {
        public int TicketCount { get; set; }
        public int UpcomingTicketCount { get; set; }
        public int PastTicketCount { get; set; }
        public int BookingCount { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
