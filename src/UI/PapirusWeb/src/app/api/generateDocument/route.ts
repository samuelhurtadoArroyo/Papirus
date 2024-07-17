import { DocumentTemplateProcess } from "@/domain/entities/DocumentTemplateProcess";
import getSession from "@/services/getSession";
import { signOut } from "../auth/[...nextauth]/auth";

const baseUrl = process.env.BASE_API_URL;

export const POST = async (request: Request) => {
  const session = await getSession();
  const res = await request.json();
  const { caseId, templateId, documentType } = res;

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
    if (!response.ok) {
      console.error("Failed to fetch document", response.statusText);
      return response;
    }
    const fileName = response?.headers
      ?.get("Content-Disposition")
      ?.split("filename")[1]
      .split('"')[1];

    const fileType = response?.headers?.get("Content-Type");

    const blob = await response.blob();

    return new Response(blob, {
      status: 200,
      headers: {
        fileName: `${fileName}`,
        fileType: `${fileType}`,
      },
    });
  } catch (error) {
    console.error("Error during document generate", error);
    return new Response((error as any)?.statusText, { status: 500 });
  }
};
