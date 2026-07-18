using TicketBox.WebUI.DTOs.TicketDtos;

namespace TicketBox.WebUI.DTOs.UserDtos
{
    public class UserDashboardPageDto
    {
        public ResultUserDashboardDto Stats { get; set; }
        public List<ResultMyTicketDto> UpcomingTickets { get; set; }
    }
}
