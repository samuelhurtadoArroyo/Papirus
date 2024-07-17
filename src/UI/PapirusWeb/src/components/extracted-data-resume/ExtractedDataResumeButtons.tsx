"use client";
import { Button } from "../common";
import { textConstants } from "@/domain/globalization/es";
import { usePermissions } from "@/hooks/usePermissions";
import { EProcessTemplates } from "@/domain/enums/EProcessTemplates";
import useToast from "@/hooks/useToast";
import { useRouter } from "next/navigation";
import { ConfirmDialog, confirmDialog } from "primereact/confirmdialog";
import { EGuardianshipDocumentTypes } from "@/domain/enums/EGuardianshipDocumentTypes";
import { ICase } from "@/domain/interfaces/ICase";
import { Toast } from "primereact/toast";
import usePendingState from "@/hooks/usePendingState";
import useFileDownload from "@/hooks/useFileDownload";
import { generateTemplate } from "@/actions/generate-template/generateTemplate";

const ExtractedDataResumeButtons = ({
  guardianshipId,
  caseData,
}: {
  guardianshipId: number;
  caseData?: ICase;
}) => {
  const { validatePermission, permissionConstants } = usePermissions();
  const { toastRef, showToast } = useToast();
  const resumeText = textConstants.pages.extractedDataResume;
  const router = useRouter();
  const { isPending, setIsPending } = usePendingState();
  const showDownloadResponseDocument = caseData?.isAnswered;
  const showDownloadEmergencyBrief = caseData?.emergencyBriefAnswered;
  const disableEmergencyBriefButton = caseData?.isAnswered;
  const { automaticDownload } = useFileDownload();

  const onClickGenerateResponseDocument = async () => {
    setIsPending(true);

    await generateTemplate({
      caseId: guardianshipId,
      templateId: EProcessTemplates.ResponseDocument,
      documentType: EGuardianshipDocumentTypes.ResponseDocument,
    }).then(async (responseState) => {
      await automaticDownload({
        path: responseState.data?.filePath,
        name: responseState.data?.fileName,
      });
      showToast(responseState);
      if (responseState.toastType === "success") {
        router.push(`/guardianships/${guardianshipId}/generated`);
      }
    });

    setIsPending(false);
  };

  const onClickGenerateEmergencyBrief = async () => {
    setIsPending(true);

    await generateTemplate({
      caseId: guardianshipId,
      templateId: EProcessTemplates.EmergencyBrief,
      documentType: EGuardianshipDocumentTypes.EmergencyBrief,
    }).then(async (responseState) => {
      await automaticDownload({
        path: responseState.data?.filePath,
        name: responseState.data?.fileName,
      });
      showToast(responseState);
      if (responseState.toastType === "success") {
        router.push(`/guardianships/${guardianshipId}/generated`);
      }
    });

    setIsPending(false);
  };

  const confirmGenerateResponseDocument = async () => {
    const accept = async () => {
      onClickGenerateResponseDocument();
    };

    confirmDialog({
      message: resumeText.confirmation.message,
      header: resumeText.confirmation.title,
      icon: "pi pi-download",
      defaultFocus: "accept",
      accept,
      reject: () => {},
      acceptLabel: resumeText.confirmation.yes,
      rejectLabel: resumeText.confirmation.no,
    });
  };

  return (
    <div className="flex gap-2 justify-center my-5 h-20 sm:h-10">
      <ConfirmDialog />
      <Toast ref={toastRef} />
      <Button
        type="button"
        className="uppercase"
        id="generate-response-document-btn"
        variant="primary"
        label={
          showDownloadResponseDocument
            ? resumeText.bottomButtons.downloadResponseDocument
            : resumeText.bottomButtons.generateResponseDocument
        }
        onClick={() =>
          caseData?.isAnswered
            ? onClickGenerateResponseDocument()
            : confirmGenerateResponseDocument()
        }
        disabled={
          isPending ||
          !validatePermission(
            permissionConstants.generateDocument.responseDocument
          )
        }
      />
      <Button
        type="button"
        className="uppercase"
        id="generate-emergency-brief-btn"
        variant="danger"
        label={
          showDownloadEmergencyBrief
            ? resumeText.bottomButtons.downloadEmergencyBrief
            : resumeText.bottomButtons.generateEmergencyBrief
        }
        onClick={onClickGenerateEmergencyBrief}
        disabled={
          isPending ||
          disableEmergencyBriefButton ||
          !validatePermission(
            permissionConstants.generateDocument.emergencyBrief
          )
        }
      />
    </div>
  );
};

export default ExtractedDataResumeButtons;
