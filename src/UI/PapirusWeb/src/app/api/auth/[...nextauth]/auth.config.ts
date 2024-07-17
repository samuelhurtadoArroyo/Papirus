import { Authentication } from "@/domain/entities/Authentication";
import { LoginInputDto } from "@/domain/entities/data-contracts";
import Credentials from "next-auth/providers/credentials";
import type { NextAuthConfig } from "next-auth";
import { authSessionConfig } from "@/domain/constants/auth";
import { jwtDecode } from "jwt-decode";
import { IUserToken } from "@/domain/interfaces/IUser";
import { Permissions } from "@/domain/entities/Permissions";

const baseUrl = process.env.BASE_API_URL;

export default {
  providers: [
    Credentials({
      async authorize(credentials) {
        const papirus = new Authentication({ baseUrl });

        const authenticationDto: LoginInputDto = {
          email: credentials?.email as string,
          password: credentials?.password as string,
        };

        try {
          const signInResponse = await papirus.v10AuthenticationLoginCreate(
            authenticationDto,
            {
              cache: "no-store",
            }
          );
          if (signInResponse?.ok && signInResponse.data.token) {
            const token = signInResponse.data.token;
            const decoded: IUserToken = jwtDecode(token);
            const user = {
              ...decoded,
              id: decoded?.nameid,
              roleId: Number(decoded?.roleId),
              firmId: Number(decoded?.firmId),
              accessToken: "Bearer " + token,
            };
            const permissionsEndpoint = new Permissions({ baseUrl });
            const permissionsResponse =
              await permissionsEndpoint.v10PermissionsGetByUserList({
                headers: {
                  Authorization: user.accessToken,
                } as HeadersInit,
              });
            const permissions = permissionsResponse?.data;

            return { ...user, permissions };
          }
        } catch (error) {
          console.error(error);
          return null;
        }

        // Return null if user data could not be retrieved
        return null;
      },
    }),
  ],
  pages: {
    signIn: "/auth/signin",
    error: "/auth/error",
  },
  callbacks: {
    async signIn({ user, account }) {
      return true;
    },
    async session({ token, session }) {
      if (token.id && session.user) {
        session.user.id = token.id;
      }

      if (session.user && token.email) {
        session.user.email = token.email;
        session.user.accessToken = token.accessToken;
        session.user.firstName = token.firstName;
        session.user.lastName = token.lastName;
        session.user.roleId = token.roleId;
        session.user.firmId = token.firmId;
        session.user.isActive = token.isActive;
        session.user.permissions = token.permissions;
      }

      return session;
    },
    async jwt({ token, user }) {
      const accessToken = user?.accessToken;

      if (accessToken) {
        const {
          firstName,
          lastName,
          roleId,
          isActive,
          email,
          firmId,
          id,
          permissions,
        } = user;

        return {
          ...token,
          permissions,
          id,
          email,
          accessToken,
          firstName,
          lastName,
          roleId,
          firmId,
          isActive,
        };
      }

      return token;
    },
  },
  session: authSessionConfig,
} satisfies NextAuthConfig;
