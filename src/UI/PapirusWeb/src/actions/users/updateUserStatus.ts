"use server";
import { Users } from "@/domain/entities/Users";
import getSession from "@/services/getSession";
import { textConstants } from "@/domain/globalization/es";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { UserDto } from "@/domain/entities/data-contracts";
import { IUserState } from "@/domain/interfaces/IFormState";

const baseUrl = process.env.BASE_API_URL;
const successMesage = textConstants.pages.editUser.success;
const errorMessage = textConstants.pages.editUser.error;

export const updateUserStatus = async (
  updatedUser: UserDto
): Promise<IUserState> => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    const userEndpoint = new Users({ baseUrl });
    const response = await userEndpoint.v10UsersUpdate(
      { ...updatedUser, id: Number(updatedUser.id) },
      {
        headers: { Authorization: session?.user?.accessToken! },
      }
    );

    if (response?.ok) {
      return {
        message: successMesage.message,
        toastTitle: successMesage.title,
        toastType: "success",
      };
    } else {
      console.error("Failed to update user", response.statusText);
      return {
        message: errorMessage.message,
        toastTitle: errorMessage.title,
        toastType: "error",
      };
    }
  } catch (error) {
    console.error("Error during user update", error);
    return {
      errors: {},
      message: errorMessage.message,
      toastTitle: errorMessage.title,
      toastType: "error",
    };
  }
};
