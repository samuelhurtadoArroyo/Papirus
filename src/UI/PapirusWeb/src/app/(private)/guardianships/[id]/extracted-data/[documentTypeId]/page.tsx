import ScrollToTop from "@/components/common/ScrollToTop";
import EmailContainer from "@/components/email/EmailContainer";
import { getCase } from "@/actions/cases/getCase";
import { EGuardianshipDocumentTypes } from "@/domain/enums/EGuardianshipDocumentTypes";
import AutoAdmitContainer from "@/components/autoadmit/AutoAdmitContainer";
import getExtractedDataByDocumentTypeId from "@/actions/extracted-data/getExtractedDataByDocumentTypeId";
import { getBusinessLines } from "@/actions/business-line/getBusinessLines";

const ExtractedDataPage = async ({
  params,
}: {
  params: { id: string; documentTypeId: string };
}) => {
  const [extractedData, caseData] = await Promise.all([
    getExtractedDataByDocumentTypeId({
      caseId: Number(params?.id),
      documentTypeId: Number(params?.documentTypeId),
    }),
    getCase(Number(params?.id)),
  ]);

  if (EGuardianshipDocumentTypes.Autoadmit === Number(params?.documentTypeId)) {
    const businessLines = await getBusinessLines();
    return (
      <AutoAdmitContainer
        lines={businessLines}
        caseData={caseData}
        fieldsData={extractedData || []}
      />
    );
  }
  return (
    <>
      <ScrollToTop />
      {EGuardianshipDocumentTypes.Email === Number(params?.documentTypeId) && (
        <EmailContainer extractedData={extractedData} caseData={caseData} />
      )}
    </>
  );
};

export default ExtractedDataPage;
