"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { Teams } from "@/domain/entities/Teams";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";
import { getTableConfigForDB } from "@/services/getTableConfigForDB";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const getTeams = async (
  lazyState: ILazyTableState,
  debouncedValue?: string
) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const teamEndpoint = new Teams({ baseUrl });

    const response = await teamEndpoint.v10TeamsList(
      {
        SearchString: debouncedValue ? debouncedValue : undefined,
        ...(lazyState ? getTableConfigForDB(lazyState) : {}),
      },
      {
        headers: {
          Authorization: session?.user?.accessToken,
        },
        cache: "no-store",
      }
    );

    if (response?.ok) {
      const totalCount = JSON.parse(
        response.headers.get("paginationdata") || ""
      )?.TotalCount;
      return { data: response?.data, totalCount };
    } else {
      console.error("Failed to fetch teams", response.statusText);
    }
  } catch (error) {
    console.error("Error during teams fetch", error);
    throw error;
  }
};
