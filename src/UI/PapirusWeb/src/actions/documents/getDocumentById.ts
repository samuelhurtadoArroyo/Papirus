import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { CaseProcessDocuments } from "@/domain/entities/CaseProcessDocuments";
import { IDocument } from "@/domain/interfaces/IDocument";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const getDocumentById = async (
  documentId: number
): Promise<IDocument | undefined> => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const userEndpoint = new CaseProcessDocuments({ baseUrl });
    const response = await userEndpoint.v10CaseProcessDocumentsDetail(
      documentId,
      {
        headers: {
          Authorization: session?.user?.accessToken,
        },
        cache: "no-store",
      }
    );

    if (response?.ok) {
      return response?.data;
    } else {
      console.error("Failed to fetch document", response.statusText);
    }
  } catch (error) {
    console.error("Error during document fetch", error);
    throw new Error("Error during document fetch");
  }
};
