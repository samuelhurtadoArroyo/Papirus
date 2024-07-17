"use server"

import { revalidateTag } from "next/cache"

export async function revalidateTeamMembersByTeamLeaderId() {
	revalidateTag("teamMembersByTeamLeaderId")
}
