"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { CaseDocumentFieldValue } from "@/domain/entities/CaseDocumentFieldValue";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const getCaseDocumentFieldValues = async (caseId: number) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  const params = {
    headers: {
      Authorization: session?.user?.accessToken,
      cache: "no-store",
    },
  };

  try {
    const caseDocumentFieldValueEndpoint = new CaseDocumentFieldValue({ baseUrl });
    const response = await caseDocumentFieldValueEndpoint.v10CaseDocumentFieldValueDetail(caseId, {}, params);

    if (response?.ok && response?.data) {
      return response?.data;
    } else {
      console.error("Failed to fields cases", response.statusText);
      throw new Error("Failed to fields cases");
    }
  } catch (error) {
    console.error("Error during fields fetch", error);
  }
};
