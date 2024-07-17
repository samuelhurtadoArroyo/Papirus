import { getViewPermissions } from "@/actions/permissions/getViewPermissions";
import TeamsContainer from "@/components/teams/TeamsContainer";
import { PERMISSIONS } from "@/domain/constants/permissions";
import { getPermissionByPermissionLabelCode } from "@/services/getPermissionByPermissionLabelCode";
import { redirect } from "next/navigation";
import React from "react";

const Teams = async () => {
  const permissions = await getViewPermissions();

  if (!getPermissionByPermissionLabelCode(permissions, PERMISSIONS.teams.view)) {
    redirect("/");
  }
  return <TeamsContainer />;
};

export default Teams;
