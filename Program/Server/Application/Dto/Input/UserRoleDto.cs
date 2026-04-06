using Domain.Interfaces;

namespace Application.Dto.Input;

public static class UserRoleDto
{
    public record Assign(
        Guid UserId,
        ushort RoleId,
        IClock Clock,
        Guid? WhoAppointedId = null
    )
    {
        public Domain.Entity.User.UserRole GetUserRole() => new(
                    userId: UserId,
                    roleId: RoleId,
                    clock: Clock,
                    whoAppointedId: WhoAppointedId);
    }

    public record Remove(
        ulong Id
    );
}
