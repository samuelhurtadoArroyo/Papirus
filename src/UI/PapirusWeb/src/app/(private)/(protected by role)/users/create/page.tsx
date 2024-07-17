import { getRoles } from "@/actions/roles/getRoles";
import { SubHeader } from "@/components/layout";
import UsersCreateForm from "@/components/users/CreateForm";
import { PERMISSIONS } from "@/domain/constants/permissions";
import { textConstants } from "@/domain/globalization/es";
import { getPermissionByPermissionLabelCode } from "@/services/getPermissionByPermissionLabelCode";
import Link from "next/link";
import { notFound, redirect } from "next/navigation";
export const dynamic = "force-dynamic";
import { getDisabledLinkProps } from "@/services/getDisabledLinkProps";
import { getViewPermissions } from "@/actions/permissions/getViewPermissions";

export default async function UsersCreatePage() {
  const userHeaderText = textConstants.pages.createUser.header;
  const permissions = await getViewPermissions();

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
        <Link
          id="return-btn"
          className="papirus-text-button"
          href={"/users"}
          {...getDisabledLinkProps(
            !getPermissionByPermissionLabelCode(permissions, PERMISSIONS.users.view)
          )}>
          {userHeaderText.button}
        </Link>
      </SubHeader>
      <UsersCreateForm roles={roles} />
    </>
  );
}
