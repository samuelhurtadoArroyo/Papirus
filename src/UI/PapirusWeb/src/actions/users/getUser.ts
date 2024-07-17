import { Users } from "@/domain/entities/Users";
import getSession from "@/services/getSession";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";

const baseUrl = process.env.BASE_API_URL;

export const getUser = async (id: string) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const userEndpoint = new Users({ baseUrl });
    const response = await userEndpoint.v10UsersDetail(Number(id), {
      headers: {
        Authorization: session?.user?.accessToken,
      },
      cache: "no-store",
    });

    if (response?.ok && response?.data) {
      return response?.data;
    } else {
      console.error("Failed to fetch user", response.statusText);
      throw new Error("Failed to fetch user");
    }
  } catch (error) {
    console.error("Error during user fetch", error);
    throw error;
  }
};
