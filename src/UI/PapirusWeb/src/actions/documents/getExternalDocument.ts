"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import getSession from "@/services/getSession";

export const getExternalDocument = async (
  filePath: string
): Promise<{ arrayBuffer: number[] } | undefined> => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const response = await fetch(filePath, {
      method: "GET",
    });

    if (response?.ok) {
      const blob = await response.blob();
      let ab = await blob.arrayBuffer();
      const object = {
        arrayBuffer: Array.from(new Uint8Array(ab)),
      };
      return object;
    } else {
      console.error("Failed to fetch documents", response.statusText);
    }
  } catch (error) {
    console.error("Error during documents fetch", error);
    throw new Error("Error during documents fetch");
  }
};
