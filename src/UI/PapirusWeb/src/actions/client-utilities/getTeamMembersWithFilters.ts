"use server";
import { signOut } from '@/app/api/auth/[...nextauth]/auth';
import { RequestParams } from '@/domain/entities/http-client';
import { TeamMembers } from '@/domain/entities/TeamMembers';
import { ILazyTableState } from '@/domain/interfaces/ILazyTableState';
import getSession from '@/services/getSession';
import { getTableConfigForDB } from '@/services/getTableConfigForDB';

const baseUrl = process.env.BASE_API_URL;

export const getTeamMembersWithFilters = async (lazyState?: ILazyTableState) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const teamMemberEndpoint = new TeamMembers({ baseUrl });
    const params = {
      headers: {
        Authorization: session?.user?.accessToken,
      } as HeadersInit,
      cache: "no-store",
    } as RequestParams;

    const response = await teamMemberEndpoint.v10TeamMembersList(
      {
        SearchString: undefined,
        ...(lazyState ? getTableConfigForDB(lazyState) : {}),
      },
      params
    );

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
