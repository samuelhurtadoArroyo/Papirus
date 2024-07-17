"use server";
import { ITeam, TeamDtoEquals } from "@/domain/interfaces/ITeam";
import { CleanTeamMemberDto, ITeamMember, TeamMemberDtoChanged } from "@/domain/interfaces/ITeamMember";
import { ToastMessage } from "primereact/toast";
import { updateTeam } from "./updateTeam";
import { createTeam } from "./createTeam";
import { createTeamMember } from "../teamMembers/createTeamMember";
import { TeamMemberDto } from "@/domain/entities/data-contracts";
import { updateTeamMember } from "../teamMembers/updateTeamMember";
import { deleteTeamMember } from "../teamMembers/deleteTeamMember";

export const saveTeamWithDetails = async (
  team: ITeam | undefined,
  teamMembers: ITeamMember[] | undefined[] | undefined,
  originalTeam: ITeam | undefined,
  originalTeamMembers: ITeamMember[] | undefined[] | undefined
) => {
  const summary = "Equipo";
  const life = 3000;
  var ret = { detail: "Ocurrió un error inesperado al guardar el equipo", severity: "error", summary, life } as ToastMessage;

  try {
    if (team?.id) {
      // Team already exists
      if (!TeamDtoEquals(team!, originalTeam!)) updateTeam(team!).then(console.debug).catch(console.error);

      const { membersToDelete, membersToUpdate, membersToCreate } = examineChanges(originalTeamMembers as ITeamMember[], teamMembers as ITeamMember[]);

      membersToDelete.forEach((tm) => deleteTeamMember(tm?.id!).then(console.debug).catch(console.error));
      membersToUpdate.forEach((tm) => updateTeamMember(tm!).then(console.debug).catch(console.error));
      membersToCreate.forEach((tm) => createTeamMember(tm!).then(console.debug).catch(console.error));

      ret = { ...ret, detail: "Grupo actualizado exitosamente.", severity: "success" } as ToastMessage;
    } else {
      // New team
      if (!TeamDtoEquals(team!, originalTeam!)) {
        let response = await createTeam(team!);
        let teamId = response?.data.id;

        if (teamId) {
          teamMembers?.forEach((tm) => {
            let { memberId, isLead, maxCases } = tm!;
            let newTeamMember = { teamId, memberId, isLead, maxCases } as TeamMemberDto;
            createTeamMember(newTeamMember).then(console.debug).catch(console.error);
          });

          ret = { ...ret, detail: response?.message, severity: "success" } as ToastMessage;
        }
      }
    }
  } catch (error) {
    console.error(error);
  }

  return ret;
};

const examineChanges = (oldTeamMemberList: ITeamMember[], newTeamMemberList: ITeamMember[]) => {
  const membersToDelete: ITeamMember[] = [];
  const membersToUpdate: ITeamMember[] = [];
  const membersToCreate: ITeamMember[] = [];

  oldTeamMemberList.forEach((oldTeamMember) => {
    let newTeamMember = newTeamMemberList.find((tm) => tm?.id == oldTeamMember?.id);

    if (!newTeamMember) {
      membersToDelete.push(CleanTeamMemberDto(oldTeamMember));
    } else if (TeamMemberDtoChanged(newTeamMember, oldTeamMember)) {
      membersToUpdate.push(CleanTeamMemberDto(newTeamMember));
    }
  });

  newTeamMemberList.forEach((ntm) => {
    if (!ntm.id || ntm.id == 0) {
      membersToCreate.push(CleanTeamMemberDto(ntm));
    }
  });

  return { membersToDelete, membersToUpdate, membersToCreate };
};
