using MovieTicketBooking.Domain.Constants.MailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTicketBooking.Application.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Message message);
    }
}
