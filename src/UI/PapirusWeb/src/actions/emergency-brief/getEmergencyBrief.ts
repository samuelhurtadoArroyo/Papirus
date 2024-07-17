"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { IDocument } from "@/domain/interfaces/IDocument";
import { emergencyBrief } from "@/mock/documentMockData";
import getSession from "@/services/getSession";

export const getEmergencyBrief = async (id: string) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    return { ...emergencyBrief, guardianshipId: Number(id) } as IDocument;
  } catch (error) {
    console.error("Error during document fetch", error);
    throw new Error("No response document found.");
  }
};
