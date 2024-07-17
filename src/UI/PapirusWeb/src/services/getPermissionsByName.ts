import { PermissionDto } from "@/domain/entities/data-contracts";

export const getPermissionsByName = (permissions: PermissionDto[], name: string) => {
  return permissions.filter((permission) => permission.name === name);
};