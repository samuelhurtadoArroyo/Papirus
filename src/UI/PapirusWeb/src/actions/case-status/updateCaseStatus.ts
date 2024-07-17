"use server";
import getSession from "@/services/getSession";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { getCorrectStatusCombinations } from "@/services/getCorrectStatusCombinations";
import { IGuardianshipState } from "@/domain/interfaces/IFormState";
import { Assign } from "@/domain/entities/Assign";

const baseUrl = process.env.BASE_API_URL;

export async function updateCaseStatus({
  caseId,
  userId,
  currentStatus,
  nextStatus,
}: {
  caseId: number;
  userId: number;
  currentStatus?: number;
  nextStatus?: number;
}): Promise<IGuardianshipState> {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    // Validate if the status transition is required
    if (
      !currentStatus ||
      !nextStatus ||
      !getCorrectStatusCombinations(currentStatus, nextStatus)
    ) {
      return {
        continue: true,
      };
    }

    const casesEndpoint = new Assign({ baseUrl });
    const response = await casesEndpoint.caseAssignmentAssignCreate(
      { caseId: caseId, userId: userId, caseStatus: nextStatus },
      {
        headers: { Authorization: session?.user?.accessToken },
      }
    );

    if (response?.ok) {
      return {
        continue: true,
      };
    } else {
      console.error("Failed to update case status", response.statusText);
      return {
        continue: false,
      };
    }
  } catch (error) {
    console.error("Error during case status update", error);
    return {
      continue: false,
    };
  }
}
