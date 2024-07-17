import { GuardianshipDto } from "../entities/data-contracts";
import { EProcessStatus } from "../enums/EProcessStatus";

export interface IGuardianship extends GuardianshipDto {
  statusId?: EProcessStatus;
  isAnswered?: boolean;
  templateIdGenerated?: number;
  emergencyBriefAnswered?: boolean;
  assignment?: {
    teamMember?: {
      memberId?: GuardianshipDto["assignedTeamMemberId"];
    };
  };
}
