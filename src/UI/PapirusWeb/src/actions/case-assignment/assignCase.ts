"use server";
import getSession from "@/services/getSession";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { RequestParams } from "@/domain/entities/http-client";
import { Assign } from "@/domain/entities/Assign";
import { IGuardianshipState } from "@/domain/interfaces/IFormState";
import { textConstants } from "@/domain/globalization/es";

const baseUrl = process.env.BASE_API_URL;
const assignText = textConstants.pages.guardianships.assign;

export const assignCase = async ({
  caseId,
  userId,
  caseStatusId,
}: {
  caseId?: number;
  userId?: number;
  caseStatusId?: number;
}): Promise<IGuardianshipState> => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    if (!caseId || !userId || !caseStatusId) {
      return {
        errors: {},
        message: assignText.error.message,
        toastTitle: assignText.error.title,
        toastType: "error",
      };
    }
    const assignCaseEndpoint = new Assign({ baseUrl });
    const params = {
      headers: {
        Authorization: session?.user?.accessToken,
      } as HeadersInit,
      cache: "no-store",
    } as RequestParams;

    const response = await assignCaseEndpoint.caseAssignmentAssignCreate(
      { caseId: caseId, userId: userId, caseStatus: caseStatusId },
      params
    );

    if (response?.ok) {
      return {
        errors: undefined,
        message: assignText.success.message,
        toastTitle: assignText.success.title,
        toastType: "success",
      };
    } else {
      console.error("Failed to assign case", response.statusText);
      return {
        errors: {},
        message: assignText.error.message,
        toastTitle: assignText.error.title,
        toastType: "error",
      };
    }
  } catch (error) {
    console.error("Error during case assignment", error);
    return {
      errors: {},
      message: assignText.error.message,
      toastTitle: assignText.error.title,
      toastType: "error",
    };
  }
};
