import { PermissionDto } from "@/domain/entities/data-contracts";

export const getPermissionByPermissionLabelCode = (permissions: PermissionDto[], permissionLabelCode: string) => {
  return permissions.find(
    (permission) => permission.permissionLabelCode === permissionLabelCode
  );
};