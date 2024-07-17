"use server";
import getSession from "@/services/getSession";
import { textConstants } from "@/domain/globalization/es";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { UpdatePasswordSchema } from "@/domain/schemas/user";
import { getNewKey } from "@/services/getNewKey";
import { IPasswordState } from "@/domain/interfaces/IFormState";

const successMesage = textConstants.pages.updatePassword.success;
const errorMessage = textConstants.pages.updatePassword.error;

export async function updatePassword(
  prevState: IPasswordState | undefined,
  formData: FormData
): Promise<IPasswordState> {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    // Validate form using Zod
    const validatedFields = UpdatePasswordSchema.safeParse({
      id: formData.get("id"),
      email: formData.get("email"),
      currentPassword: formData.get("currentPassword"),
      newPassword: formData.get("newPassword"),
      confirmPassword: formData.get("confirmPassword"),
    });

    // If form validation fails, return errors early. Otherwise, continue.
    if (!validatedFields.success) {
      console.error(
        "Missing or wrong fields. Failed to update Password.",
        validatedFields.error.flatten().fieldErrors
      );
      return {
        errors: validatedFields.error.flatten().fieldErrors,
        message: "Missing or wrong fields. Failed to update Password.",
      };
    }

    return {
      ...prevState,
      errors: undefined,
      message: successMesage.message,
      toastTitle: successMesage.title,
      toastType: "success",
      resetFormKey: getNewKey(),
      redirect: true,
    };
  } catch (error) {
    console.error("Error during user update", error);
    return {
      ...prevState,
      errors: {},
      message: errorMessage.message,
      toastTitle: errorMessage.title,
      toastType: "error",
      resetFormKey: getNewKey(),
      redirect: false,
    };
  }
}
