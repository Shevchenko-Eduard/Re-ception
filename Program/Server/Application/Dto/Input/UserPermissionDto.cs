using Domain.Entity.User;
using Domain.Interfaces;

namespace Application.Dto.Input;

public static class UserPermissionDto
{
    public record Assign(
        Guid UserId,
        ushort PermissionId,
        IClock Clock,
        Guid? WhoAppointedId = null
    )
    {
        public Domain.Entity.User.UserPermission GetUserPermission()
        {
            UserPermission userPermission = new(
                    userId: UserId,
                    roleId: PermissionId,
                    clock: Clock);
            if (WhoAppointedId is not null)
            {
                userPermission.AddAuthor((Guid)WhoAppointedId);
            }
            return userPermission;
        }
    }

    public record Remove(
        ulong Id
    );
}
