import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { CaseDocumentFieldValue } from "@/domain/entities/CaseDocumentFieldValue";
import { IExtractedDataResume } from "@/domain/interfaces/IExtractedData";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const getExtractedData = async ({ caseId }: { caseId: number }) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }
  try {
    const extractedDataEndpoint = new CaseDocumentFieldValue({ baseUrl });
    const response =
      await extractedDataEndpoint.v10CaseDocumentFieldValueDetail(
        caseId,
        undefined,
        {
          headers: {
            Authorization: session?.user?.accessToken,
          },
          cache: "no-store",
        }
      );

    if (response?.ok && response?.data) {
      const extractedDataResume: IExtractedDataResume[] = response.data.reduce(
        (acc: IExtractedDataResume[], item: any) => {
          const { documentTypeId, documentTypeName } = item;
          const existingData = acc.find(
            (data) => data.documentTypeId === documentTypeId
          );

          if (existingData) {
            existingData?.extractedData.push(item);
          } else {
            acc.push({
              documentTypeId,
              documentTypeName,
              extractedData: [item],
            });
          }

          return acc;
        },
        []
      );

      return extractedDataResume || [];
    } else {
      console.error("Failed to fetch extracted data000");
      return [];
    }
  } catch (error) {
    console.error("Error during extracted data fetch", error);
    throw new Error("Error during extracted data fetch");
  }
};

export default getExtractedData;
