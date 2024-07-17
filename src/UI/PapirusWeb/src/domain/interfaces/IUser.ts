import { PermissionDto, UserDto } from "@/domain/entities/data-contracts";

export interface IUser extends Omit<UserDto, "id"> {
  id: string;
  name?: string | null;
  accessToken?: string | null;
  mustChangePassword?: boolean;
  permissions?: PermissionDto[];
}

export interface IUserToken {
	nameid: string;
	email: string;
	name: string;
	roleId: string;
	firmId: string;
}