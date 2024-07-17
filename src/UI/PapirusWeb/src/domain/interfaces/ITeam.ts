import { TeamDto } from "@/domain/entities/data-contracts";

export interface ITeam extends TeamDto {}

export const TeamDtoEquals: (a: ITeam, b: ITeam) => boolean = (a: ITeam, b: ITeam): boolean => a.id === b.id && a.name === b.name;

export const CleanTeamDto: (a: ITeam) => TeamDto = (a: ITeam): TeamDto => ({ ...a, id: Number(a.id) } as ITeam);
