"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { DocumentTemplateProcess } from "@/domain/entities/DocumentTemplateProcess";
import { EGuardianshipDocumentTypes } from "@/domain/enums/EGuardianshipDocumentTypes";
import { EProcessTemplates } from "@/domain/enums/EProcessTemplates";
import { textConstants } from "@/domain/globalization/es";
import { ITemplateState } from "@/domain/interfaces/IFormState";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;
const generateTemplateText =
  textConstants.pages.generatedDocument.onDocumentGenerate;

export const generateTemplate = async ({
  caseId,
  templateId,
  documentType,
}: {
  caseId: number;
  templateId: EProcessTemplates;
  documentType: EGuardianshipDocumentTypes;
}): Promise<ITemplateState> => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const permissionsEndpoint = new DocumentTemplateProcess({ baseUrl });
    const response = await permissionsEndpoint.v10DocumentTemplateProcessCreate(
      caseId,
      {
        templateId,
        documenType: documentType,
      },
      {
        headers: {
          Authorization: session?.user?.accessToken,
        },
      }
    );

    if (response?.ok && response?.data) {
      return {
        message: generateTemplateText.success.message,
        toastType: "success",
        toastTitle: generateTemplateText.success.title,
        data: response?.data,
      };
    } else {
      console.error("Failed to generate document", response.statusText);
      return {
        errors: {},
        message: generateTemplateText.error.message,
        toastType: "error",
        toastTitle: generateTemplateText.error.title,
      };
    }
  } catch (error) {
    console.error("Error during document generate", error);
    return {
      errors: {},
      message: generateTemplateText.error.message,
      toastType: "error",
      toastTitle: generateTemplateText.error.title,
    };
  }
};
