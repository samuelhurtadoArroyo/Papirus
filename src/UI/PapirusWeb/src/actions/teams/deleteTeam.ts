"use server";

import { Teams } from "@/domain/entities/Teams";
import getSession from "@/services/getSession";
import { textConstants } from "@/domain/globalization/es";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { RequestParams } from "@/domain/entities/http-client";

const baseUrl = process.env.BASE_API_URL;
const successMesage = textConstants.components.messages.success;

export const deleteTeam = async (teamId: number) => {
  try {
    const session = await getSession();
    const teamEndpoint = new Teams({ baseUrl });

    const Authorization = session?.user?.accessToken;
    const headers = { Authorization };
    const params = { headers } as RequestParams;

    if (!session || !session.user?.accessToken) {
      signOut();
      throw new Error("No active session or access token found.");
    }

    const response = await teamEndpoint.v10TeamsDelete(teamId, params);

    if (response?.ok) {
      return { message: successMesage.deleteTeam };
    } else {
      console.error("Failed to delete team", response.statusText);
      return { error: response.statusText };
    }
  } catch (error) {
    console.error("Error during team delete", error);
    return { error: "No se pudo eliminar el equipo" };
  }
};
