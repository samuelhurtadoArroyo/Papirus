"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { textConstants } from "@/domain/globalization/es";
import { ITeamMember } from "@/domain/interfaces/ITeamMember";
import { TeamMembers } from "@/domain/entities/TeamMembers";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;
const successMesage = textConstants.components.messages.success;

export const createTeamMember = async (newTeamMember: ITeamMember) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const teamMemberEndpoint = new TeamMembers({ baseUrl });

    const response = await teamMemberEndpoint.v10TeamMembersCreate(
      newTeamMember,
      {
        headers: { Authorization: session?.user?.accessToken },
      }
    );

    if (response?.ok) {
      return { message: successMesage.createTeamMember };
    } else {
      console.error("Failed to create team member", response.statusText);
    }
  } catch (error) {
    console.error("Error during team member creation", error);
  }
};
