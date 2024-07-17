import ExtractedDataTable from "../extracted-data/ExtractedDataTable";
import ExtractedDataResumeButtons from "./ExtractedDataResumeButtons";
import { getBusinessLines } from "@/actions/business-line/getBusinessLines";
import { getCase } from "@/actions/cases/getCase";
import getExtractedData from "@/actions/extracted-data/getExtractedData";
import { textConstants } from "@/domain/globalization/es";

const ExtractedDataResume = async ({
  guardianshipId,
}: {
  guardianshipId: number;
}) => {
  const [extractedData, caseData, lines] = await Promise.all([
    getExtractedData({ caseId: guardianshipId }),
    getCase(guardianshipId),
    getBusinessLines(),
  ]);
  //Add business line to autoadmit information
  extractedData[0]?.extractedData?.push({
    fieldValue: lines?.find((line) => caseData?.businessLineId === line.id)
      ?.name,
    name: textConstants.pages.autoadmit.businessLine,
  });

  return (
    <>
      <div className="flex flex-col gap-10 w-full">
        {extractedData?.map((data) => (
          <ExtractedDataTable
            key={data.documentTypeId}
            title={data.documentTypeName ? data.documentTypeName : ""}
            data={data.extractedData}
          />
        ))}
      </div>
      <ExtractedDataResumeButtons
        guardianshipId={guardianshipId}
        caseData={caseData}
      />
    </>
  );
};

export default ExtractedDataResume;
