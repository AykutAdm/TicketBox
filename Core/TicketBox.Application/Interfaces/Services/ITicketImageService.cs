using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Models;

namespace TicketBox.Application.Interfaces.Services
{
    public interface ITicketImageService
    {
        byte[] GenerateTicketImage(TicketImageData data);
        string SaveTicketImage(TicketImageData data, string webRootPath);
    }
}
