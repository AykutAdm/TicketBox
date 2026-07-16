using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBox.Application.Features.Mediator.Events.Commands;

namespace TicketBox.Application.Features.Mediator.Events.Validators
{
    public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
    {
        public UpdateEventCommandValidator()
        {

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Etkinlik başlığı boş olamaz.")
                .MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.");

            RuleFor(x => x.EventDate)
                .GreaterThan(DateTime.Now).WithMessage("Etkinlik tarihi ileri bir tarih olmalı.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Lokasyon boş olamaz.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Kontenjan 0'dan büyük olmalı.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Fiyat negatif olamaz.");
        }
    }
}
