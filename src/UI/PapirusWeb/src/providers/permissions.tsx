"use client";
import { PermissionDto } from "@/domain/entities/data-contracts";
import React, { createContext } from "react";

export const PermissionsContext = createContext<any[]>([]);

export default function PermissionsProvider({
  permissions,
  children,
}: {
  permissions: PermissionDto[];
  children: React.ReactNode;
}) {
  return (
    <PermissionsContext.Provider value={permissions}>
      {children}
    </PermissionsContext.Provider>
  );
}
