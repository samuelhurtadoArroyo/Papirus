import { getPermissions } from "@/actions/permissions/getPermissions";
import ScrollToTop from "@/components/common/ScrollToTop";
import { SubHeader } from "@/components/layout";
import { PERMISSIONS } from "@/domain/constants/permissions";
import { textConstants } from "@/domain/globalization/es";
import { getDisabledLinkProps } from "@/services/getDisabledLinkProps";
import { getPermissionByPermissionLabelCode } from "@/services/getPermissionByPermissionLabelCode";
import Link from "next/link";
import DocumentsContainer from "@/components/documents/DocumentsContainer";
import { Suspense } from "react";
import SuspenseLoading from "@/components/common/SuspenseLoading";

export default async function GuardianshipsDocumentsPage({
  params,
}: {
  params: { id: string };
}) {
  const permissions = await getPermissions();
  const validatePermission = (name: string) => {
    const permission = getPermissionByPermissionLabelCode(permissions, name);
    return !!permission;
  };

  const guardianshipsText = textConstants.pages.documents.header;

  return (
    <>
      <ScrollToTop />
      <SubHeader title={guardianshipsText.title}>
        <Link
          id="return-btn"
          className="papirus-text-button"
          href={"/guardianships"}
          {...getDisabledLinkProps(
            !validatePermission(PERMISSIONS.guardianships.view)
          )}>
          {guardianshipsText.button}
        </Link>
      </SubHeader>
      <Suspense fallback={<SuspenseLoading />}>
        <DocumentsContainer guardianshipId={Number.parseInt(params?.id)} />
      </Suspense>
    </>
  );
}
