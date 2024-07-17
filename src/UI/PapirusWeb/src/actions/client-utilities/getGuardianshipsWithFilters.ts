"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { Cases } from "@/domain/entities/Cases";
import { ILazyTableState } from "@/domain/interfaces/ILazyTableState";
import getSession from "@/services/getSession";
import { getTableConfigForDB } from "@/services/getTableConfigForDB";

const baseUrl = process.env.BASE_API_URL;

export const getGuardianshipsWithFilters = async ({
  lazyState,
  debouncedValue,
}: {
  lazyState?: ILazyTableState;
  debouncedValue?: string;
}) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const guardianshipsEndpoint = new Cases({ baseUrl });
    const response = await guardianshipsEndpoint.v10CasesGuardianshipsList(
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
      )?.TotalCount || 0;
      return { data: response?.data, totalCount };
    } else {
      console.error("Failed to fetch guardianships", response.statusText);
    }
  } catch (error) {
    console.error("Error during guardianships fetch", error);
  }
};
