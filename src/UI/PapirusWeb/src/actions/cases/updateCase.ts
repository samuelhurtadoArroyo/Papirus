"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { Cases } from "@/domain/entities/Cases";
import getSession from "@/services/getSession";
import { CaseDto } from "@/domain/entities/data-contracts";

const baseUrl = process.env.BASE_API_URL;

export const updateCase = async (_case: CaseDto) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const Authorization = session?.user?.accessToken;
    const cache = "no-store";
    const headers = { Authorization, cache };
    const params = { headers };
    const casesEndpoint = new Cases({ baseUrl });
    const response = await casesEndpoint.v10CasesUpdate(_case, params);

    if (response?.ok && response?.data) {
      return response?.data;
    } else {
      const errorText = "Failed to update case " + _case.id;
      console.error(errorText, response.statusText);
      throw new Error(errorText);
    }
  } catch (error: any) {
    console.error("Error during cases fetch", error);
    if ("error" in error && "errors" in error?.error && error?.error?.errors) {
      throw new Error(error?.error?.errors);
    }
  }
};
