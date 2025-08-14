using Hostal.Application.UsesCases.Reservation.DTOs.CommandsDto;
using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;

public class CreateReservationCommand : ReservationCommandDto, IRequest;
