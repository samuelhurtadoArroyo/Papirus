"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { textConstants } from "@/domain/globalization/es";
import { ITeamMember } from "@/domain/interfaces/ITeamMember";
import { TeamMembers } from "@/domain/entities/TeamMembers";
import { RequestParams } from "@/domain/entities/http-client";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;
const successMesage = textConstants.components.messages.success;

export const updateTeamMember = async (updatedTeamMember: ITeamMember) => {
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
    const response = await teamMemberEndpoint.v10TeamMembersUpdate(
      { ...updatedTeamMember, id: Number(updatedTeamMember.id) },
      params
    );

    if (response?.ok) {
      return { message: successMesage.updateTeamMember };
    } else {
      console.error("Failed to update team", response.statusText);
    }
  } catch (error) {
    console.error("Error during team update", error);
  }
};
