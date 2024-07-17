"use server";
import { Users } from "@/domain/entities/Users";
import getSession from "@/services/getSession";
import { textConstants } from "@/domain/globalization/es";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { EditUserSchema } from "@/domain/schemas/user";
import { IUserState } from "@/domain/interfaces/IFormState";

const baseUrl = process.env.BASE_API_URL;
const successMesage = textConstants.pages.editUser.success;
const errorMessage = textConstants.pages.editUser.error;

export async function updateUser(
  prevState: IUserState | undefined,
  formData: FormData
): Promise<IUserState> {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    // Validate form using Zod
    const validatedFields = EditUserSchema.safeParse({
      id: formData.get("id"),
      firstName: formData.get("firstName"),
      lastName: formData.get("lastName"),
      email: formData.get("email"),
      roleId: formData.get("roleId"),
      password: formData.get("password"),
      confirmPassword: formData.get("confirmPassword"),
    });

    // If form validation fails, return errors early. Otherwise, continue.
    if (!validatedFields.success) {
      console.error(
        "Missing or wrong fields. Failed to update User.",
        validatedFields.error.flatten().fieldErrors
      );
      return {
        errors: validatedFields.error.flatten().fieldErrors,
        message: "Missing or wrong fields. Failed to update User.",
        toastTitle: undefined,
        toastType: undefined,
      };
    }

    const { firstName, lastName, email, roleId, id } = validatedFields.data;

    const userEndpoint = new Users({ baseUrl });
    const response = await userEndpoint.v10UsersUpdate(
      {
        firstName,
        lastName,
        email,
        roleId,
        id,
      },
      {
        headers: { Authorization: session?.user?.accessToken! },
      }
    );

    if (response?.ok) {
      return {
        ...prevState,
        errors: undefined,
        message: successMesage.message,
        toastTitle: successMesage.title,
        toastType: "success",
      };
    } else {
      console.error("Failed to update user", response.statusText);
      return {
        errors: {},
        message: errorMessage.message,
        toastTitle: errorMessage.title,
        toastType: "error",
      };
    }
  } catch (error: any) {
    console.error("Error during user update", error);
    return {
      errors: {},
      message: error?.error?.errors,
      toastTitle: errorMessage.title,
      toastType: "error",
    };
  }
}
