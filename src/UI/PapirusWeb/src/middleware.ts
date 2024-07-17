import NextAuth from "next-auth";

import authConfig from "@/app/api/auth/[...nextauth]/auth.config";

import {
  DEFAULT_LOGIN_REDIRECT,
  GUARDIANSHIPS_ROUTES,
  AUTH_ROUTES,
  PUBLIC_ROUTES,
  DEFAULT_MUST_CHANGE_PASSWORD_REDIRECT,
  TEAMS_ROUTES,
  CONFIGS_ROUTES,
  DEMANDS_ROUTES,
  SETTINGS_ROUTES,
  USERS_ROUTES
} from "@/domain/constants/routes";
import { isRouteFromList } from "./services/isRouteFromList";
import { getPermissionsByType } from './services/getPermissionsByType';
import { PERMISSIONS } from "./domain/constants/permissions";
import { getPermissionByPermissionLabelCode } from "./services/getPermissionByPermissionLabelCode";

const { auth } = NextAuth(authConfig);

export default auth((req) => {
  const { nextUrl } = req;
  const user = req?.auth?.user;
  const viewPermissions = getPermissionsByType(user?.permissions || [], "view")
  const isLoggedIn = !!req.auth;

  const isPublicRoute = isRouteFromList(PUBLIC_ROUTES, nextUrl.pathname);
  const isGuardianshipsRoute = isRouteFromList(GUARDIANSHIPS_ROUTES, nextUrl.pathname);
  const isTeamsRoute = isRouteFromList(TEAMS_ROUTES, nextUrl.pathname);
  const isConfigsRoute = isRouteFromList(CONFIGS_ROUTES, nextUrl.pathname);
  const isDemandsRoute = isRouteFromList(DEMANDS_ROUTES, nextUrl.pathname);
  const isSettingsRoute = isRouteFromList(SETTINGS_ROUTES, nextUrl.pathname);
  const isUsersRoute = isRouteFromList(USERS_ROUTES, nextUrl.pathname);
  const isAuthRoute = isRouteFromList(AUTH_ROUTES, nextUrl.pathname);

  if (
    !getPermissionByPermissionLabelCode(
      viewPermissions,
      PERMISSIONS.guardianships.view
    ) &&
    isGuardianshipsRoute
  ) {
    // redirect to dashboard page if user does not have view permissions
    return Response.redirect(new URL(DEFAULT_LOGIN_REDIRECT, nextUrl));
  }

  if (
    !getPermissionByPermissionLabelCode(
      viewPermissions,
      PERMISSIONS.teams.view
    ) &&
    isTeamsRoute
  ) {
    // redirect to dashboard page if user does not have view permissions
    return Response.redirect(new URL(DEFAULT_LOGIN_REDIRECT, nextUrl));
  }

  if (
    !getPermissionByPermissionLabelCode(
      viewPermissions,
      PERMISSIONS.config.view
    ) &&
    isConfigsRoute
  ) {
    // redirect to dashboard page if user does not have view permissions
    return Response.redirect(new URL(DEFAULT_LOGIN_REDIRECT, nextUrl));
  }

  if (
    !getPermissionByPermissionLabelCode(
      viewPermissions,
      PERMISSIONS.demands.view
    ) &&
    isDemandsRoute
  ) {
    // redirect to dashboard page if user does not have view permissions
    return Response.redirect(new URL(DEFAULT_LOGIN_REDIRECT, nextUrl));
  }

  if (
    !getPermissionByPermissionLabelCode(
      viewPermissions,
      PERMISSIONS.users.view
    ) &&
    (isUsersRoute || isSettingsRoute)
  ) {
    // redirect to dashboard page if user does not have view permissions
    return Response.redirect(new URL(DEFAULT_LOGIN_REDIRECT, nextUrl)); 
  }

  if (
    user?.mustChangePassword &&
    DEFAULT_MUST_CHANGE_PASSWORD_REDIRECT !== nextUrl.pathname
  ) {
    // redirect to update password page if user must change password
    return Response.redirect(
      new URL(DEFAULT_MUST_CHANGE_PASSWORD_REDIRECT, nextUrl)
    );
  } else if (
    !user?.mustChangePassword &&
    DEFAULT_MUST_CHANGE_PASSWORD_REDIRECT === nextUrl.pathname
  ) {
    // redirect to dashboard page if user alrady changed password at least once
    return Response.redirect(new URL(DEFAULT_LOGIN_REDIRECT, nextUrl));
  }

  if (isAuthRoute) {
    if (isLoggedIn) {
      // redirect to dashboard page if user is already logged in and acces auth routes
      return Response.redirect(new URL(DEFAULT_LOGIN_REDIRECT, nextUrl));
    }
  }

  if (!isLoggedIn && !isPublicRoute && !isAuthRoute) {
    return Response.redirect(new URL(`/auth/signin`, nextUrl));
  }
});

export const config = {
  matcher: ["/((?!api|_next/static|_next/image|favicon.ico).*)"],
};
