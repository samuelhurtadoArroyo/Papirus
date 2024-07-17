"use server";

import { DEFAULT_LOGIN_REDIRECT } from "@/domain/constants/routes";
import { signIn } from "@/app/api/auth/[...nextauth]/auth";
import { AuthError } from "next-auth";
import { textConstants } from "@/domain/globalization/es";
import { LoginSchema } from "@/domain/schemas/auth";
import { getNewKey } from "@/services/getNewKey";
import { IAuthState } from "@/domain/interfaces/IFormState";

export const login = async (formData: FormData): Promise<IAuthState> => {
  const errorText = textConstants.components.messages.error;
  const sucessText = textConstants.components.messages.success;

  try {
    // Validate form using Zod
    const validatedFields = LoginSchema.safeParse({
      email: formData.get("email"),
      password: formData.get("password"),
    });

    // If form validation fails, return errors early. Otherwise, continue.
    if (!validatedFields.success) {
      console.error(
        "Missing or wrong fields. Failed to sign in.",
        validatedFields.error.flatten().fieldErrors
      );
      return {
        errors: validatedFields.error.flatten().fieldErrors,
        message: errorText.credentialsError,
        toastType: "error",
        toastTitle: errorText.signin,
        resetFormKey: getNewKey(),
      };
    }

    const { email, password } = validatedFields.data;

    await signIn("credentials", {
      email,
      password,
      redirectTo: DEFAULT_LOGIN_REDIRECT,
    });

    return {
      message: sucessText.successAction,
      resetFormKey: getNewKey(),
    };
  } catch (error) {
    if (error instanceof AuthError) {
      switch (error.type) {
        case "CredentialsSignin":
          console.error("Credentials error", error);
          return {
            errors: {
              email: [errorText.credentialsError],
              password: [errorText.credentialsError],
            },
            message: errorText.credentialsError,
            toastType: "error",
            toastTitle: errorText.signin,
            resetFormKey: getNewKey(),
          };
        default:
          console.error("Error during login", error);
          return {
            errors: {
              email: [errorText.credentialsError],
              password: [errorText.credentialsError],
            },
            message: errorText.credentialsSignin,
            toastType: "error",
            toastTitle: errorText.signin,
            resetFormKey: getNewKey(),
          };
      }
    }
    throw error;
  }
};
