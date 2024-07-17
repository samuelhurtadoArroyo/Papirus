import { getRoles } from "@/actions/roles/getRoles";
import { InputSearchParams } from "@/components/common";
import { SubHeader } from "@/components/layout";
import UsersTable from "@/components/users/UsersTable";
import { textConstants } from "@/domain/globalization/es";
import { getPermissionByPermissionLabelCode } from "@/services/getPermissionByPermissionLabelCode";
import Link from "next/link";
import { PERMISSIONS } from "@/domain/constants/permissions";
import { getDisabledLinkProps } from "@/services/getDisabledLinkProps";
import { notFound, redirect } from "next/navigation";
import { getPermissions } from "@/actions/permissions/getPermissions";

export default async function UsersPage() {
  const userHeaderText = textConstants.pages.users.header;
  const permissions = await getPermissions();

  if (!getPermissionByPermissionLabelCode(permissions, PERMISSIONS.users.view)) {
    redirect("/");
  }

  const roles = await getRoles();

  if (!roles) {
    return notFound();
  }
  
  return (
    <>
      <SubHeader title={userHeaderText.title}>
        <InputSearchParams
          placeholder={userHeaderText.search}
          label={userHeaderText.search}
          disabled={
            !getPermissionByPermissionLabelCode(
              permissions,
              PERMISSIONS.users.search
            )
          }
        />
        <Link
          id="add-btn"
          className="papirus-text-button"
          href={"/users/create"}
          {...getDisabledLinkProps(
            !getPermissionByPermissionLabelCode(
              permissions,
              PERMISSIONS.users.create
            )
          )}>
          {userHeaderText.button}
        </Link>
      </SubHeader>
      <UsersTable roles={roles} />
    </>
  );
}
