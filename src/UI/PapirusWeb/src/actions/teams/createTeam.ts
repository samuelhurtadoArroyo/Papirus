"use server";
import { Teams } from "@/domain/entities/Teams";
import getSession from "@/services/getSession";
import { textConstants } from "@/domain/globalization/es";
import { ITeam } from "@/domain/interfaces/ITeam";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { RequestParams } from "@/domain/entities/http-client";

const baseUrl = process.env.BASE_API_URL;
const successMesage = textConstants.components.messages.success;

export const createTeam = async (newUser: ITeam) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const teamEndpoint = new Teams({ baseUrl });
    const Authorization = session?.user?.accessToken;
    const headers = { Authorization };
    const params = { headers } as RequestParams;
    const response = await teamEndpoint.v10TeamsCreate(newUser, params);

    if (response?.ok) {
      let message = successMesage.createTeam;
      let { data } = response;

      return { message, data };
    } else {
      console.error("Failed to create team", response.statusText);
    }
  } catch (error) {
    console.error("Error during team creation", error);
  }
};
