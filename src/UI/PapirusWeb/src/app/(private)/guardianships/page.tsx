import { getPermissions } from "@/actions/permissions/getPermissions";
import { InputSearchParams } from "@/components/common";
import GuardianshipsTable from "@/components/guardianships/GuardianshipsTable";
import { SubHeader } from "@/components/layout";
import { PERMISSIONS } from "@/domain/constants/permissions";
import { textConstants } from "@/domain/globalization/es";
import { getPermissionByPermissionLabelCode } from "@/services/getPermissionByPermissionLabelCode";
import { redirect } from "next/navigation";

export default async function GuardianshipsPage() {
  const guardianshipsText = textConstants.pages.guardianships.header;
  const permissions = await getPermissions();

  if (!getPermissionByPermissionLabelCode(permissions, PERMISSIONS.guardianships.view)) {
    redirect("/");
  }

  return (
    <>
      <SubHeader title={guardianshipsText.title}>
        <InputSearchParams
          placeholder={guardianshipsText.search}
          label={guardianshipsText.search}
          disabled={
            !getPermissionByPermissionLabelCode(
              permissions,
              PERMISSIONS.guardianships.search
            )
          }
        />
      </SubHeader>
      <GuardianshipsTable/>
    </>
  );
}
