"use client";
import { SubmitForm } from "@/components/common";
import { textConstants } from "@/domain/globalization/es";
import { useRouter } from "next/navigation";
import { useCallback, useState, useTransition } from "react";
import { IDocument } from "@/domain/interfaces/IDocument";
import { IGuardianshipState } from "@/domain/interfaces/IFormState";
import { usePermissions } from "@/hooks/usePermissions";
import { EProcessStatus } from "@/domain/enums/EProcessStatus";
import { updateCaseStatus } from "@/actions/case-status/updateCaseStatus";
import { EGuardianshipDocumentTypes } from "@/domain/enums/EGuardianshipDocumentTypes";
import useFileDownload from "@/hooks/useFileDownload";
import { useCurrentUser } from "@/hooks/useCurrentUser";

const DocumentsButtons = ({
  guardianshipId,
  guardianshipStatusId,
  userId,
  documents,
}: {
  guardianshipId: number;
  guardianshipStatusId: number;
  userId?: number;
  documents: IDocument[];
}) => {
  const [isPending, startTransition] = useTransition();
  const [isDownloading, setIsDownloading] = useState(false);
  const router = useRouter();
  const user = useCurrentUser();
  const { validatePermission, permissionConstants } = usePermissions();
  const buttonsText = textConstants.pages.documents.bottomButtons;
  const { automaticMultipleDownload } = useFileDownload();

  const redirectCallback = useCallback(
    (state: IGuardianshipState) => {
      state?.continue &&
        router.push(
          `/guardianships/${guardianshipId}/extracted-data/${EGuardianshipDocumentTypes.Autoadmit}`
        );
    },
    [guardianshipId, router]
  );

  const FormAction = async () => {
    if (user?.id && Number.parseInt(user?.id) !== userId) {
      await startTransition(() => {
        redirectCallback({ continue: true });
      });
      return;
    }
    await startTransition(() => {
      userId &&
        updateCaseStatus({
          caseId: guardianshipId,
          userId: userId,
          currentStatus: guardianshipStatusId,
          nextStatus: EProcessStatus.EnProgreso,
        }).then((state) => {
          redirectCallback(state);
        });
    });
  };

  const onDownload = async () => {
    setIsDownloading(true);
    await automaticMultipleDownload(documents);
    setIsDownloading(false);
  };

  return (
    <form className="w-full" action={FormAction}>
      <SubmitForm
        secondaryId={"download-btn"}
        primaryId={"continue-btn"}
        secondaryText={buttonsText.download}
        primaryText={buttonsText.continue}
        onClickSecondary={onDownload}
        disablePrimary={
          isDownloading ||
          isPending ||
          !validatePermission(permissionConstants.extractedData.view)
        }
        disableSecondary={
          isDownloading ||
          isPending ||
          !validatePermission(permissionConstants.documents.download)
        }
      />
    </form>
  );
};

export default DocumentsButtons;
