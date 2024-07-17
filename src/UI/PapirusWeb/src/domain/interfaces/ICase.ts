import { CaseDto, TeamMemberDto } from "../entities/data-contracts";
import { EProcessStatus } from "../enums/EProcessStatus";

export interface ICase extends CaseDto {
  assignedTeamMemberId?: TeamMemberDto["memberId"];
  memberId?: number;
  assignedTeamMemberName?: string;
  statusId?: EProcessStatus;
}
