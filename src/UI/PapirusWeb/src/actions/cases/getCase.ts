import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { Cases } from "@/domain/entities/Cases";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const getCase = async (caseId: number) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const Authorization = session?.user?.accessToken;
    const cache = "no-store";
    const headers = { Authorization };
    const casesEndpoint = new Cases({ baseUrl });
    const response = await casesEndpoint.v10CasesDetail(caseId, {
      headers,
      cache,
    });

    if (response?.ok && response?.data) {
      return response?.data;
    } else {
      console.error("Failed to fetch cases", response.statusText);
      throw new Error("Failed to fetch cases");
    }
  } catch (error) {
    console.error("Error during cases fetch", error);
  }
};
