using Domain.Entity.Reservation;
using Domain.Interfaces.Repositories.BaseRepository;

namespace Domain.Interfaces.Repositories.ReservationRepository;

public interface IReservationStatusRepository : IBaseStatusObjectRepository<ReservationStatus>
{

}