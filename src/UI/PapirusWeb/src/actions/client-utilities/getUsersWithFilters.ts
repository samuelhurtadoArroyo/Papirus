"use server";
import { Users } from "@/domain/entities/Users";
import getSession from "@/services/getSession";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";
import { getTableConfigForDB } from "@/services/getTableConfigForDB";

const baseUrl = process.env.BASE_API_URL;

export const getUsersWithFilters = async ({
  lazyState,
  debouncedValue,
}: {
  lazyState: ILazyTableState;
  debouncedValue?: string;
}) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }
  try {
    const userEndpoint = new Users({ baseUrl });
    const response = await userEndpoint.v10UsersList(
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
      console.error("Failed to fetch users", response.statusText);
    }
  } catch (error) {
    console.error("Error during users fetch", error);
  }
};
