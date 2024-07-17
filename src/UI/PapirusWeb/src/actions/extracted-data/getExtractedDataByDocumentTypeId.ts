import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { CaseDocumentFieldValue } from "@/domain/entities/CaseDocumentFieldValue";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const getExtractedDataByDocumentTypeId = async ({
  caseId,
  documentTypeId,
}: {
  caseId: number;
  documentTypeId?: number;
}) => {
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
        { documentTypeId },
        {
          headers: {
            Authorization: session?.user?.accessToken,
          },
          cache: "no-store",
        }
      );

    if (response?.ok) {
      return response?.data || [];
    } else {
      console.error(
        "Failed to fetch extracted data by document",
        response.statusText
      );
    }
  } catch (error) {
    console.error("Error during users extracted data by document", error);
  }
};

export default getExtractedDataByDocumentTypeId;
