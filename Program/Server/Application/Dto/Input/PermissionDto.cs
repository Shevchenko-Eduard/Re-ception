using Domain.Entity.User.Permission;

namespace Application.Dto.Input;

public static class PermissionDto
{
    public record Create(
        byte ActionId,
        byte EntityId,
        byte FlagId
    )
    {
        public Permission GetPermission() => new(
            actionId: ActionId,
            entityId: EntityId,
            flagId: FlagId
        );
    }

    public record Delete(
        byte Id
    );
}
