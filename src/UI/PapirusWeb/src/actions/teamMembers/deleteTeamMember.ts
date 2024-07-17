"use server";

import getSession from "@/services/getSession";
import { textConstants } from "@/domain/globalization/es";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { RequestParams } from "@/domain/entities/http-client";
import { TeamMembers } from "@/domain/entities/TeamMembers";

const baseUrl = process.env.BASE_API_URL;
const successMesage = textConstants.components.messages.success;

export const deleteTeamMember = async (teamMemberId: number) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const teamMemberEndpoint = new TeamMembers({ baseUrl });
    const Authorization = session?.user?.accessToken;
    const headers = { Authorization };
    const params = { headers } as RequestParams;
    const response = await teamMemberEndpoint.v10TeamMembersDelete(
      teamMemberId,
      params
    );

    if (response?.ok) {
      return { message: successMesage.deleteTeamMember };
    } else {
      console.error("Failed to delete team", response.statusText);
      return { error: response.statusText };
    }
  } catch (error) {
    console.error("Error during team delete", error);
    return { error: "No se pudo eliminar el equipo" };
  }
};
