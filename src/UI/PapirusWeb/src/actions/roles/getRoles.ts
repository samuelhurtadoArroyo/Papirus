import { Roles } from "@/domain/entities/Roles";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const getRoles = async (roleName?: string) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const rolesEndpoint = new Roles({ baseUrl });
    const response = await rolesEndpoint.v10RolesList(
      { SearchString: roleName },
      {
        headers: {
          Authorization: session?.user?.accessToken,
        },
        cache: "force-cache",
      }
    );

    if (response?.ok) {
      return response?.data;
    } else {
      console.error("Failed to fetch roles", response.statusText);
      throw new Error("Failed to fetch roles");
    }
  } catch (error) {
    console.error("Error during roles fetch", error);
    throw error;
  }
};
