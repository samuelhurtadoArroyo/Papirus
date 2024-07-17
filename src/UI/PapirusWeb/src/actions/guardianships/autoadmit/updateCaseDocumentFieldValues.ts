"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { CaseDocumentFieldValue } from "@/domain/entities/CaseDocumentFieldValue";
import { CaseDocumentFieldValueDto, UpdateCaseDocumentFieldValueDto } from "@/domain/entities/data-contracts";
import { textConstants } from "@/domain/globalization/es";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const updateCaseDocumentFieldValues = async (fields: CaseDocumentFieldValueDto[]) => {
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
    const updatedFields = fields.map((field) => {
      const { id, fieldValue } = field;
      return { id, fieldValue } as UpdateCaseDocumentFieldValueDto;
    });

    const response = await caseDocumentFieldValueEndpoint.v10CaseDocumentFieldValueUpdate(updatedFields, params);

    if (response?.ok) {
      return textConstants.pages.autoadmit.onFieldsUpdates.successMessage;
    } else {
      console.error("Failed to update fields", response.statusText);
      throw new Error("Failed to update fields");
    }
  } catch (error) {
    console.error("Error during update fields", error);
  }
};
