import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { Cases } from "@/domain/entities/Cases";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const getGuardianship = async (id: number) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const guardianshipsEndpoint = new Cases({ baseUrl });
    const response = await guardianshipsEndpoint.v10CasesGuardianshipsList(
      {
        FilterParams: [
          {
            columnName: "id",
            filterValue: String(id),
            filterOption: "IsEqualTo",
          },
        ],
      },
      {
        headers: {
          Authorization: session?.user?.accessToken,
        },
        cache: "no-store",
      }
    );
    if (response?.ok && response?.data && response?.data.length > 0) {
      return response?.data[0];
    } else {
      console.error("Failed to fetch guardianship", response.statusText);
    }
  } catch (error) {
    console.error("Error during guardianship fetch", error);
  }
};
