import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { BusinessLine } from "@/domain/entities/BusinessLine";
import getSession from "@/services/getSession";

const baseUrl = process.env.BASE_API_URL;

export const getBusinessLines = async () => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const businessLineEndpoint = new BusinessLine({ baseUrl });
    const response = await businessLineEndpoint.v10BusinessLineList({
      headers: {
        Authorization: session?.user?.accessToken,
      },
      cache: "force-cache",
    });

    if (response?.ok && response?.data) {
      const { data } = response;

      return data;
    } else {
      console.error("Failed to fetch cases", response.statusText);
      throw new Error("Failed to fetch cases");
    }
  } catch (error) {
    console.error("Error during cases fetch", error);
  }
};
