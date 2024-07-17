import { PermissionsContext } from "@/providers/permissions";
import { useCallback, useContext } from "react";
import { PERMISSIONS } from "@/domain/constants/permissions";
import { getPermissionByPermissionLabelCode } from "@/services/getPermissionByPermissionLabelCode";

export const usePermissions = () => {
  const permissions = useContext(PermissionsContext);

  const validatePermission = useCallback(
    (permissionLabelCode: string) => {
      const permission = getPermissionByPermissionLabelCode(
        permissions,
        permissionLabelCode
      );
      return !!permission;
    },
    [permissions]
  );
  
  return {
    validatePermission,
    getPermissionByPermissionLabelCode,
    permissionConstants: PERMISSIONS,
  };
};
