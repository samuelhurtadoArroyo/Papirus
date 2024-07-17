"use server";
import { Users } from "@/domain/entities/Users";
import getSession from "@/services/getSession";
import { textConstants } from "@/domain/globalization/es";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { CreateUserSchema } from "@/domain/schemas/user";
import { getNewKey } from "@/services/getNewKey";
import { IUserState } from "@/domain/interfaces/IFormState";

const baseUrl = process.env.BASE_API_URL;
const successMesage = textConstants.pages.createUser.success;
const errorMessage = textConstants.pages.createUser.error;

export async function createUser(
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
    const validatedFields = CreateUserSchema.safeParse({
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
        "Missing or wrong fields. Failed to create Account.",
        validatedFields.error.flatten().fieldErrors
      );
      return {
        errors: validatedFields.error.flatten().fieldErrors,
        message: "Missing or wrong fields. Failed to create Account.",
      };
    }

    const { firstName, lastName, email, password, roleId } =
      validatedFields.data;

    const userEndpoint = new Users({ baseUrl });
    const response = await userEndpoint.v10UsersCreate(
      { firstName, lastName, email, password, roleId },
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
        resetFormKey: getNewKey(),
      };
    } else {
      console.error("Failed to update user", response.statusText);
      return {
        ...prevState,
        errors: {},
        message: errorMessage.message,
        toastTitle: errorMessage.title,
        toastType: "error",
      };
    }
  } catch (error) {
    console.error("Error during user update", error);
    return {
      ...prevState,
      errors: {},
      message: errorMessage.message,
      toastTitle: errorMessage.title,
      toastType: "error",
    };
  }
}
