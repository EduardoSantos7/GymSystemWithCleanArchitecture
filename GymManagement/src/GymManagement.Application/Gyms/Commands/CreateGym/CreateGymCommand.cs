using ErrorOr;
using GymManagement.Application.Common.Authorization;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Commands.CreateGym;

[Authorize(Roles = "Admin")]
public record CreateGymCommand(string Name, Guid SubscriptionId) : IRequest<ErrorOr<Gym>>;