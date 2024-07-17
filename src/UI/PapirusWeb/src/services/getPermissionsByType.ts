import { PermissionDto } from "@/domain/entities/data-contracts";

export const getPermissionsByType = (
	permissions: PermissionDto[],
	type: string
) => {
  return permissions.filter((permission) =>
    permission.permissionLabelCode?.includes(type)
  );
};
