"use server";
import { Teams } from "@/domain/entities/Teams";
import getSession from "@/services/getSession";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { RequestParams } from "@/domain/entities/http-client";

const baseUrl = process.env.BASE_API_URL;

export const getTeamMembersByTeamId = async (teamId: number) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const teamEndpoint = new Teams({ baseUrl });
    const params = {
      headers: {
        Authorization: session?.user?.accessToken,
      } as HeadersInit,
      cache: "no-store",
    } as RequestParams;

    const response = await teamEndpoint.v10TeamsMembersDetail(teamId, params);

    if (response?.ok) {
      return response?.data;
    } else {
      console.error("Failed to fetch team members", response.statusText);
    }
  } catch (error) {
    console.error("Error during team members fetch", error);
    throw error;
  }
};
