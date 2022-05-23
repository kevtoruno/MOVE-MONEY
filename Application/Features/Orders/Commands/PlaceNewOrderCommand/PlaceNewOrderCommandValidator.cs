using Application.Core;
using Application.Core.Dtos;
using Application.Core.Interface;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands;

public class PlaceNewOrderCommandValidor : AbstractValidator<PlaceNewOrderCommand>
{
    public PlaceNewOrderCommandValidor()
    {
        RuleFor(v => v.OrderToCreateDto.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be higher than 0.");

        RuleFor(v => v.OrderToCreateDto.RecipientId)
            .Equal(v => v.OrderToCreateDto.SenderId)
            .WithMessage("Sender and recipient are the same!");
    }
}

