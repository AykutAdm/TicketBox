using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Bookings.Commands;

namespace TicketBox.Application.Features.Mediator.Bookings.Validators
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("Geçerli bir etkinlik seçilmeli.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Bilet adedi en az 1 olmalı.")
                .LessThanOrEqualTo(10).WithMessage("Tek seferde en fazla 10 bilet alabilirsiniz.");
        }
    }
}
