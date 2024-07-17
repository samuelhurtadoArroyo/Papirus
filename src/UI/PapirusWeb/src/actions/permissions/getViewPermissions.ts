import { Permissions } from "@/domain/entities/Permissions";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import getSession from "@/services/getSession";
import { getPermissionsByType } from "@/services/getPermissionsByType";

const baseUrl = process.env.BASE_API_URL;

export const getViewPermissions = async () => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const permissionsEndpoint = new Permissions({ baseUrl });
    const response = await permissionsEndpoint.v10PermissionsGetByUserList({
      headers: {
        Authorization: session?.user?.accessToken,
      },
      next: { revalidate: 0 },
    });

    if (response?.ok) {
      return getPermissionsByType(response?.data, "view");
    } else {
      console.error("Failed to fetch permissions", response.statusText);
      throw new Error("Failed to fetch permissions");
    }
  } catch (error) {
    console.error("Error during permissions fetch", error);
    throw error;
  }
};
