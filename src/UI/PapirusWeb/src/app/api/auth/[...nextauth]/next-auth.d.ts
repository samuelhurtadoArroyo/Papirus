import type { Session, User } from "next-auth";
import type { JWT } from "next-auth/jwt";
import { IUser } from "@/domain/interfaces/IUser";
import { AdapterUser } from "next-auth/adapters";

declare module "next-auth/jwt" {
  interface JWT extends IUser {
    id?: string;
  }
}

declare module "next-auth" {
  interface Session {
    user: IUser;
  }
  interface User extends IUser {}
}

declare module "next-auth/adapters" {
  interface AdapterUser extends IUser {
    emailVerified?: Date | null;
  }
}
