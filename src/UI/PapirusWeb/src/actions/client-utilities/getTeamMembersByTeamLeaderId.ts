"use server";
import getSession from "@/services/getSession";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { GetListTeamMembers } from "@/domain/entities/GetListTeamMembers";
import { ITeamMemberAssignment } from "@/domain/interfaces/ITeamMember";

const baseUrl = process.env.BASE_API_URL;

export const getTeamMembersByTeamLeaderId = async (
  teamMemberId: number
): Promise<ITeamMemberAssignment[] | undefined> => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const teamEndpoint = new GetListTeamMembers({ baseUrl });

    const response = await teamEndpoint.caseAssignmentGetListTeamMembersList(
      { teamMemberId },
      {
        headers: {
          Authorization: session?.user?.accessToken,
        },
        cache: "no-store",
      }
    );

    if (response?.ok) {
      return (response.data as ITeamMemberAssignment[]) || [];
    } else {
      console.error(
        "Failed to fetch team members by team member id",
        response.statusText
      );
    }
  } catch (error) {
    console.error("Error during team members by team member id fetch", error);
  }
};
