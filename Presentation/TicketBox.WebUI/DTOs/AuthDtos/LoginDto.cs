namespace TicketBox.WebUI.DTOs.AuthDtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
