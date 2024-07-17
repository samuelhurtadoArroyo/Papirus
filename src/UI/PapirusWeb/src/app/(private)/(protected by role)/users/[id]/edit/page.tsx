import { getPermissions } from "@/actions/permissions/getPermissions";
import { getRoles } from "@/actions/roles/getRoles";
import { getUser } from "@/actions/users/getUser";
import { SubHeader } from "@/components/layout";
import UsersEditForm from "@/components/users/EditForm";
import { PERMISSIONS } from "@/domain/constants/permissions";
import { textConstants } from "@/domain/globalization/es";
import { getPermissionByPermissionLabelCode } from "@/services/getPermissionByPermissionLabelCode";
import Link from "next/link";
import { getDisabledLinkProps } from "@/services/getDisabledLinkProps";
import { getViewPermissions } from "@/actions/permissions/getViewPermissions";
import { notFound, redirect } from "next/navigation";
export const dynamic = "force-dynamic";

export default async function UsersEditPage({
  params,
}: {
  params: { id: string };
}) {
  const id = params.id;
  const userHeaderText = textConstants.pages.createUser.header;
  const permissions = await getViewPermissions();

  if (!getPermissionByPermissionLabelCode(permissions, PERMISSIONS.users.view)) {
    redirect("/");
  }

  const roles = await getRoles();

  if (!roles) {
    return notFound();
  }

  const user = await getUser(id);

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
      <UsersEditForm user={user} roles={roles} />
    </>
  );
}
