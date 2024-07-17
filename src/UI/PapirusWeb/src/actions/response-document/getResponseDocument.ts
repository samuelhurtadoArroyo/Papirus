"use server";
import { signOut } from "@/app/api/auth/[...nextauth]/auth";
import { IDocument } from "@/domain/interfaces/IDocument";
import { responseDocument } from "@/mock/documentMockData";
import getSession from "@/services/getSession";

export const getResponseDocument = async (id: string) => {
  const session = await getSession();

  if (!session || !session.user?.accessToken) {
    signOut();
    throw new Error("No active session or access token found.");
  }

  try {
    return { ...responseDocument, guardianshipId: Number(id) } as IDocument;
  } catch (error) {
    console.error("Error during document fetch", error);
    throw new Error("No response document found.");
  }
};
