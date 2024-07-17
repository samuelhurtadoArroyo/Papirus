import {
  TeamMemberAssignmentDto,
  TeamMemberDto,
} from "@/domain/entities/data-contracts";

export interface ITeamMember extends TeamMemberDto {}

export interface ITeamMemberAssignment extends TeamMemberAssignmentDto {}

export const TeamMemberDtoEquals: (a: ITeamMember, b: ITeamMember) => boolean = (a: ITeamMember, b: ITeamMember): boolean =>
  a.id === b.id && a.teamId === b.teamId && a.memberId === b.memberId && a.isLead === b.isLead && a.maxCases === b.maxCases;

export const TeamMemberDtoChanged: (a: ITeamMember, b: ITeamMember) => boolean = (a: ITeamMember, b: ITeamMember): boolean => a.id === b.id && !TeamMemberDtoEquals(a, b);

export const CleanTeamMemberDto: (tm: ITeamMember) => TeamMemberDto = (tm: ITeamMember): TeamMemberDto => ({ ...tm, member: undefined, id: Number(tm?.id ?? 0) } as ITeamMember);
